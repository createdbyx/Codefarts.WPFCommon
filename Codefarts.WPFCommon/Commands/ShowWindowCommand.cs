namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class ShowWindowCommand : ICommand
    {
        private Window parentwindow;
        private bool showDialog;

        public ShowWindowCommand(bool showDialog, Window parentwindow)
        {
            this.showDialog = showDialog;
            this.parentwindow = parentwindow;
        }

        public bool CanExecute(object parameter)
        {
            return parameter != null && parameter is Window;
        }

        public void Execute(object parameter)
        {
            var window = parameter as Window;
            if (window == null)
            {
                return;
            }

            if (this.parentwindow != null)
            {
                window.Owner = this.parentwindow;
            }

            if (this.showDialog)
            {
                window.Show();
            }
            else
            {
                window.ShowDialog();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}