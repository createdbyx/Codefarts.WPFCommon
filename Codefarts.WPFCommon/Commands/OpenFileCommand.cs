// <copyright file="OpenFileCommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>


namespace Codefarts.WPFCommon.Commands
{
    using System;
#if NETCOREAPP3_1
    using Microsoft.Win32;
    using System.Windows;
#else
    using System.Windows.Forms;
#endif
    using System.Windows.Media;

    public class OpenFileCommand : DelegateCommand
    {
        public Action<string> FileSelected
        {
            get; set;
        }

        public string SelectedFile
        {
            get; set;
        }

        public string Filter
        {
            get; set;
        }

        public bool ExpectsOwnerWindow
        {
            get; set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public OpenFileCommand(bool expectsOwnerWindow)
            : this()
        {
            this.ExpectsOwnerWindow = expectsOwnerWindow;
        }

        public OpenFileCommand(Action<string> fileSelectedCallback)
            : this()
        {
            this.FileSelected = fileSelectedCallback;
        }

        public OpenFileCommand(string file, Action<string> pathSelectedCallback)
            : this(pathSelectedCallback)
        {
            this.SelectedFile = file;
        }

        public OpenFileCommand(string file, string filter, Action<string> pathSelectedCallback)
            : this(pathSelectedCallback)
        {
            this.SelectedFile = file;
            this.Filter = filter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public OpenFileCommand()
        {
#if NETCOREAPP3_1
            this.CanExecuteCallback = parameter => !this.ExpectsOwnerWindow || (this.ExpectsOwnerWindow && parameter is Window);
#else
            this.CanExecuteCallback = parameter => !this.ExpectsOwnerWindow || (this.ExpectsOwnerWindow && parameter is Visual);
#endif
            this.ExecuteCallback = parameter =>
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = this.Filter;
                dialog.FileName = this.SelectedFile;
#if NETCOREAPP3_1
                var window = parameter as Window;
                var result = !this.ExpectsOwnerWindow ? dialog.ShowDialog() : dialog.ShowDialog(window);
                if (result.HasValue && result.Value)
#else
                var window = parameter as Visual;
                var result = !this.ExpectsOwnerWindow ? dialog.ShowDialog() : dialog.ShowDialog(HelpersFunctions.GetIWin32Window(window));
                if (result == DialogResult.OK)
#endif
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

        public OpenFileCommand(Action<string> pathSelectedCallback, string filter, bool expectsOwnerWindow)
            : this(pathSelectedCallback)
        {
            this.Filter = filter;
            this.ExpectsOwnerWindow = expectsOwnerWindow;
        }

        public OpenFileCommand(Action<string> pathSelectedCallback, string filter)
            : this(pathSelectedCallback)
        {
            this.Filter = filter;
        }
    }
}