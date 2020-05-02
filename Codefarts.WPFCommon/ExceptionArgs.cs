namespace Codefarts.WPFCommon
{
    using System;

    public class ExceptionArgs : EventArgs
    {
        public Exception Exception { get; set; }
                     
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.EventArgs"/> class.
        /// </summary>
        public ExceptionArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }
}