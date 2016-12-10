namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class SetClipboardTextCommand : ICommand
    {
        public SetClipboardTextCommand(TextDataFormat dataFormat)
        {
            DataFormat = dataFormat;
        }

        public TextDataFormat DataFormat { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetClipboardTextCommand"/> class.
        /// </summary>
        public SetClipboardTextCommand()
        {
            this.DataFormat = TextDataFormat.Text;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Clipboard.SetText(parameter.ToString(), this.DataFormat);
        }

        public event EventHandler CanExecuteChanged;
    }
}