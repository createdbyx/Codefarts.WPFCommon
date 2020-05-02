namespace Codefarts.WPFCommon.Commands
{
    using System.ComponentModel;

    public delegate void DownloadFileCommandProgressEventHandler(object sender, DownloadFileCommandProgressEventHandlerArgs args);

    public class DownloadFileCommandProgressEventHandlerArgs : CancelEventArgs
    {
        public float Progress { get; private set; }

        public int BytesPerSecond { get; private set; }

        public DownloadFileCommandProgressEventHandlerArgs()
            : base()
        {
        }

        public DownloadFileCommandProgressEventHandlerArgs(float progress, int bytesPerSecond)
            : base()
        {
            this.Progress = progress;
            this.BytesPerSecond = bytesPerSecond;
        }

        public DownloadFileCommandProgressEventHandlerArgs(bool cancel, float progress, int bytesPerSecond)
            : base(cancel)
        {
            this.Progress = progress;
            this.BytesPerSecond = bytesPerSecond;
        }

        public DownloadFileCommandProgressEventHandlerArgs(bool cancel)
            : base(cancel)
        {
        }
    }
}