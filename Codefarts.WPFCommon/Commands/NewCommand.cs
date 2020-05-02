namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Windows;

    public class NewCommand : DelegateCommand
    {
        private bool isDirty;
        private Action doSave;
        private Action doNew;

        public Action DoNew
        {
            get
            {
                return this.doNew;
            }
        }

        public Action DoSave
        {
            get
            {
                return this.doSave;
            }
        }

        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
        }

        public override void Execute(object parameter)
        {
            Action action = null;
            if (this.isDirty)
            {
                switch (MessageBox.Show("You have unsaved changes! Do you want to save the changes?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning))
                {
                    case MessageBoxResult.Cancel:
                        return;

                    case MessageBoxResult.Yes:
                        action = this.DoSave;
                        if (action != null)
                        {
                            action();
                        }
                        action = this.DoNew;
                        if (action != null)
                        {
                            action();
                        }
                        break;

                    case MessageBoxResult.No:
                        action = this.DoNew;
                        if (action != null)
                        {
                            action();
                        }
                        this.isDirty = false;
                        break;
                }

                return;
            }

            action = this.DoNew;
            if (action != null)
            {
                action();
            }
        }
    }
}