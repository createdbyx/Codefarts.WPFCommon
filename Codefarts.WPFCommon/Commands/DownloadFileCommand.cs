// <copyright file="DownloadFileCommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.IO;
    using System.Net;

    /// <summary>
    ///     Provides a commend for downloading files.
    /// </summary>
    public class DownloadFileCommand : DelegateCommand
    {
        public event EventHandler Error;

        private int bufferSize;

        private DateTime ifModifiedSince = DateTime.MinValue;

        private Stream outputStream;

        private Uri url;

        public event DownloadFileCommandMessageEventHandler Message;

        public event DownloadFileCommandProgressEventHandler Progress;

        public DownloadFileCommand(Uri url, Stream outputStream)
        {
            this.url = url ?? throw new ArgumentNullException(nameof(url));
            this.outputStream = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
        }

        public DownloadFileCommand()
        {
        }

        public DownloadFileCommand(Func<object, bool> canExecuteCallback, Action<object> executeCallback)
            : base(canExecuteCallback, executeCallback)
        {
        }

        public DownloadFileCommand(Uri url, Stream outputStream, int bufferSize)
        {
            this.url = url ?? throw new ArgumentNullException(nameof(url));
            this.outputStream = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
            this.bufferSize = bufferSize;
        }

        public Uri Url
        {
            get
            {
                return this.url;
            }

            set
            {
                var currentValue = this.url;
                if (currentValue != value)
                {
                    this.url = value;
                    this.NotifyOfPropertyChange(() => this.Url);
                }
            }
        }

        public DateTime IfModifiedSince
        {
            get
            {
                return this.ifModifiedSince;
            }

            set
            {
                var currentValue = this.ifModifiedSince;
                if (currentValue != value)
                {
                    this.ifModifiedSince = value;
                    this.NotifyOfPropertyChange(() => this.IfModifiedSince);
                }
            }
        }

        public Stream OutputStream
        {
            get
            {
                return this.outputStream;
            }

            set
            {
                var currentValue = this.outputStream;
                if (currentValue != value)
                {
                    this.outputStream = value;
                    this.NotifyOfPropertyChange(() => this.OutputStream);
                }
            }
        }

        public int BufferSize
        {
            get
            {
                return this.bufferSize;
            }

            set
            {
                var currentValue = this.bufferSize;
                if (currentValue != value)
                {
                    this.bufferSize = value;
                    this.NotifyOfPropertyChange(() => this.BufferSize);
                }
            }
        }

        public override void Execute(object parameters)
        {
            var errorOccoured = false;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(this.url);

                this.OnMessage("Requesting data: " + this.url);

                if (this.ifModifiedSince != DateTime.MinValue)
                {
                    request.IfModifiedSince = this.ifModifiedSince;
                }

                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    var contentLength = httpResponse.ContentLength;
                    this.OnMessage(string.Format("{0} bytes to read.", contentLength));

                    using (var writer = new BinaryWriter(this.outputStream))
                    {
                        using (var reader = new BinaryReader(httpResponse.GetResponseStream()))
                        {
                            var readCount = 0;
                            var chunk = reader.ReadBytes(this.bufferSize);
                            var startTime = DateTime.Now;
                            while (chunk.Length > 0)
                            {
                                writer.Write(chunk, 0, chunk.Length);
                                readCount += chunk.Length;

                                // Displays the operation identifier, and the transfer progress.
                                this.OnMessage(
                                    string.Format(
                                        "downloaded {0} of {1} bytes. {2} % complete...",
                                        readCount,
                                        contentLength,
                                        Math.Min(readCount / contentLength * 100, 100)));

                                // (readCount) bytes per second divided by ( (startTime) divided by ticks per second to get number of seconds elapsed )
                                var bytesPerSecond =
                                    readCount / TimeSpan.FromTicks(DateTime.Now.Ticks - startTime.Ticks).TotalSeconds;

                                // reset time after x seconds to keep relevant
                                if (DateTime.Now > startTime + TimeSpan.FromSeconds(1))
                                {
                                    startTime = DateTime.Now;
                                }

                                bool cancel;
                                this.OnProgress(Math.Min((float)readCount / contentLength * 100f, 100f), (int)bytesPerSecond, out cancel);
                                if (cancel)
                                {
                                    this.OnMessage("Download canceled. " + this.url);
                                    break;
                                }

                                chunk = reader.ReadBytes(this.bufferSize);
                            }

                            reader.Close();
                        }

                        writer.Flush();
                    }

                    this.OnMessage("Success.");
                }
            }
            catch (WebException we)
            {
                errorOccoured = true;
                var httpResponse = we.Response as HttpWebResponse;
                if (httpResponse == null)
                {
                    this.OnMessage(string.Format("Error downloading! Status: {0}", we.Status), (int?)httpResponse.StatusCode);
                }
                else
                {
                    switch (httpResponse.StatusCode)
                    {
                        case HttpStatusCode.NotModified:
                            this.OnMessage("StatusCode: 304 Not modified no need to download!", (int?)httpResponse.StatusCode);
                            break;

                        case HttpStatusCode.NotFound:
                            this.OnMessage(string.Format("StatusCode: 404 Status: {0} Not found!", we.Status), (int?)httpResponse.StatusCode);
                            break;

                        default:
                            this.OnMessage(
                                string.Format("StatusCode: {0}  Status: {1} ", httpResponse.StatusCode, we.Status) + we.Message,
                                (int?)httpResponse.StatusCode);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccoured = true;
                this.OnMessage("Unknown Error downloading", -1);
            }

            if (errorOccoured)
            {
                this.OnError();
            }
        }

        protected void OnMessage(string message, int? code = 0)
        {
            var handler = this.Message;
            if (handler != null)
            {
                handler(this, new DownloadFileCommandMessageEventHandlerArgs(message, code ?? 0));
            }
        }

        private void OnProgress(float progress, int bytesPerSecond, out bool cancel)
        {
            var callback = this.Progress;
            if (callback != null)
            {
                var args = new DownloadFileCommandProgressEventHandlerArgs(progress, bytesPerSecond);
                callback(this, args);
                cancel = args.Cancel;
                return;
            }

            cancel = false;
        }

        protected virtual void OnError()
        {
            var handler = this.Error;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}