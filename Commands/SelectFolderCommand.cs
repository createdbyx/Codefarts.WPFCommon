namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Windows.Forms;

    public class SelectFolderCommand : DelegateCommand
    {
        private string selectedPath;

        public Action<string> PathSelected { get; set; }

        public string SelectedPath
        {
            get
            {
                return this.selectedPath;
            }

            set
            {
                this.selectedPath = value;
            }
        }

        public bool ExpectsOwnerWindow { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public SelectFolderCommand(bool expectsOwnerWindow) : this()
        {
            this.ExpectsOwnerWindow = expectsOwnerWindow;
        }

        public SelectFolderCommand(Action<string> pathSelectedCallback) : this()
        {
            this.PathSelected = pathSelectedCallback;
        }

        public SelectFolderCommand(string path, Action<string> pathSelectedCallback) : this(pathSelectedCallback)
        {
            this.selectedPath = path;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public SelectFolderCommand()
        {
            this.CanExecuteCallback = parameter => !this.ExpectsOwnerWindow || (this.ExpectsOwnerWindow && parameter is IWin32Window);
            this.ExecuteCallback = parameter =>
            {
                var dialog = new FolderBrowserDialog();
                dialog.SelectedPath = this.selectedPath;
                var result = !this.ExpectsOwnerWindow ? dialog.ShowDialog() : dialog.ShowDialog(parameter as IWin32Window);
                if (result == DialogResult.OK)
                {
                    this.selectedPath = dialog.SelectedPath;
                    var action = this.PathSelected;
                    if (action != null)
                    {
                        action(this.SelectedPath);
                    }
                }
            };
        }
    }
}