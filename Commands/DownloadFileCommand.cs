// <copyright>
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
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
        public delegate void MessagesCallback(string message, int code);

        public delegate bool ProgressCallback(float progress, int bytesPerSecond, out bool cancel);

        public event EventHandler Error;

        private int bufferSize;

        private DateTime ifModifiedSince = DateTime.MinValue;

        private MessagesCallback messageCallback;

        private Stream outputStream;

        private ProgressCallback reportProgressCallback;

        private Uri url;

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

        public DownloadFileCommand(Uri url, Stream outputStream, int bufferSize, ProgressCallback reportProgressCallback, MessagesCallback messageCallback)
        {
            this.url = url ?? throw new ArgumentNullException(nameof(url));
            this.outputStream = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
            this.bufferSize = bufferSize;
            this.reportProgressCallback = reportProgressCallback ??
                                          throw new ArgumentNullException(nameof(reportProgressCallback));
            this.messageCallback = messageCallback ?? throw new ArgumentNullException(nameof(messageCallback));
        }

        public Uri Url
        {
            get
            {
                return this.url;
            }

            set
            {
                var pValue = this.url;
                if (pValue != value)
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
                var pValue = this.ifModifiedSince;
                if (pValue != value)
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
                var pValue = this.outputStream;
                if (pValue != value)
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
                var pValue = this.bufferSize;
                if (pValue != value)
                {
                    this.bufferSize = value;
                    this.NotifyOfPropertyChange(() => this.BufferSize);
                }
            }
        }

        public ProgressCallback ReportProgressCallback
        {
            get
            {
                return this.reportProgressCallback;
            }

            set
            {
                var callback = this.reportProgressCallback;
                if (callback != value)
                {
                    this.reportProgressCallback = value;
                    this.NotifyOfPropertyChange(() => this.ReportProgressCallback);
                }
            }
        }

        public MessagesCallback MessageCallback
        {
            get
            {
                return this.messageCallback;
            }

            set
            {
                var pValue = this.messageCallback;
                if (pValue != value)
                {
                    this.messageCallback = value;
                    this.NotifyOfPropertyChange(() => this.MessageCallback);
                }
            }
        }

        public override void Execute(object parameters)
        {
            var errorOccoured = false;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(this.url);

                this.SendMessage(this.url.ToString());

                //var ifModifiedSince = request.IfModifiedSince;
                if (this.ifModifiedSince != DateTime.MinValue)
                {
                    request.IfModifiedSince = this.ifModifiedSince;
                }

                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    var contentLength = httpResponse.ContentLength;
                    this.SendMessage(string.Format("{0} bytes to read.", contentLength));

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
                                this.SendMessage(
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
                                this.SendProgress(Math.Min((float)readCount / contentLength * 100f, 100f), (int)bytesPerSecond, out cancel);
                                if (cancel)
                                {
                                    this.SendMessage("Download canceled. " + this.url);
                                    break;
                                }

                                chunk = reader.ReadBytes(this.bufferSize);
                            }

                            reader.Close();
                        }

                        writer.Flush();
                    }

                    this.SendMessage("Success.");
                }
            }
            catch (WebException we)
            {
                errorOccoured = true;
                var httpResponse = we.Response as HttpWebResponse;
                if (httpResponse == null)
                {
                    this.SendMessage(string.Format("Error downloading! Status: {0}", we.Status), (int?)httpResponse.StatusCode);
                }
                else
                {
                    switch (httpResponse.StatusCode)
                    {
                        case HttpStatusCode.NotModified:
                            this.SendMessage("StatusCode: 304 Not modified no need to download!", (int?)httpResponse.StatusCode);
                            break;

                        case HttpStatusCode.NotFound:
                            this.SendMessage(string.Format("StatusCode: 404 Status: {0} Not found!", we.Status), (int?)httpResponse.StatusCode);
                            break;

                        default:
                            this.SendMessage(
                                string.Format("StatusCode: {0}  Status: {1} ", httpResponse.StatusCode, we.Status) + we.Message,
                                (int?)httpResponse.StatusCode);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccoured = true;
                this.SendMessage("Unknown Error downloading", -1);
            }

            if (errorOccoured)
            {
                this.OnError();
            }

            // return new CommandResult<string>() { Successful = true };
        }

        private void SendMessage(string message, int? code = 0)
        {
            var callback = this.messageCallback;
            if (callback != null)
            {
                callback(message, code.HasValue ? code.Value : 0);
            }
        }

        private void SendProgress(float progress, int bytesPerSecond, out bool cancel)
        {
            var callback = this.reportProgressCallback;
            if (callback != null)
            {
                callback(progress, bytesPerSecond, out cancel);
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