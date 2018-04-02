namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Media;

    public class SaveAsCommand : DelegateCommand
    {
        public Action<string> FileSelected { get; set; }

        public string SelectedFile { get; set; }

        public string Filter { get; set; }

        public bool ExpectsOwnerWindow { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAsCommand"/> class.
        /// </summary>
        public SaveAsCommand(bool expectsOwnerWindow)
            : this()
        {
            this.ExpectsOwnerWindow = expectsOwnerWindow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAsCommand"/> class.
        /// </summary>
        /// <param name="fileSelectedCallback">The callback to be called when a file has been succesfully selected.</param>
        public SaveAsCommand(Action<string> fileSelectedCallback)
            : this()
        {
            this.FileSelected = fileSelectedCallback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAsCommand"/> class.
        /// </summary>
        /// <param name="fileSelectedCallback">The callback to be called when a file has been succesfully selected.</param>
        public SaveAsCommand(bool expectsOwnerWindow, Action<string> fileSelectedCallback)
            : this(fileSelectedCallback)
        {
            this.ExpectsOwnerWindow = expectsOwnerWindow;
        }

        public SaveAsCommand(string file, Action<string> pathSelectedCallback)
                : this(pathSelectedCallback)
        {
            this.SelectedFile = file;
        }

        public SaveAsCommand(string file, string filter, Action<string> pathSelectedCallback)
            : this(pathSelectedCallback)
        {
            this.SelectedFile = file;
            this.Filter = filter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAsCommand"/> class.
        /// </summary>
        public SaveAsCommand()
        {
            this.CanExecuteCallback = parameter => !this.ExpectsOwnerWindow || (this.ExpectsOwnerWindow && parameter is Visual);
            this.ExecuteCallback = parameter =>
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = this.Filter;
                dialog.FileName = this.SelectedFile;
                var window = parameter as Visual;
                var result = !this.ExpectsOwnerWindow ? dialog.ShowDialog() : dialog.ShowDialog(HelpersFunctions.GetIWin32Window(window));
                //   var result = !this.ExpectsOwnerWindow ? dialog.ShowDialog() : dialog.ShowDialog(parameter as IWin32Window);
                if (result == DialogResult.OK)
                {
                    this.SelectedFile = dialog.FileName;
                    var action = this.FileSelected;
                    if (action != null)
                    {
                        action(this.SelectedFile);
                    }
                }
            };
        }
    }
}
