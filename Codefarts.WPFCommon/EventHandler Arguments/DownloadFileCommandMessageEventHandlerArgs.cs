namespace Codefarts.WPFCommon.Commands
{
    using System;

    public delegate void DownloadFileCommandMessageEventHandler(object sender, DownloadFileCommandMessageEventHandlerArgs args);

    public class DownloadFileCommandMessageEventHandlerArgs : EventArgs
    {
        public int Code { get; private set; }

        public DownloadFileCommandMessageEventHandlerArgs(string message, int code)
        {
            this.Message = message;
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileCommandMessageEventHandlerArgs"/> class.
        /// </summary>
        public DownloadFileCommandMessageEventHandlerArgs()
        {
        }

        public DownloadFileCommandMessageEventHandlerArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}