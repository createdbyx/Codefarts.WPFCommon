// <copyright file="SaveAsCommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.WPFCommon.Commands
{
    using System;
#if NETCOREAPP3_1_OR_GREATER
    using System.Windows;
    using Microsoft.Win32;
#else
    using System.Windows.Forms;
#endif

    public class SaveAsCommand : DelegateCommand
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
        /// <param name="fileSelectedCallback">The callback to be called when a file has been successfully selected.</param>
        public SaveAsCommand(Action<string> fileSelectedCallback)
            : this()
        {
            this.FileSelected = fileSelectedCallback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAsCommand"/> class.
        /// </summary>
        /// <param name="fileSelectedCallback">The callback to be called when a file has been successfully selected.</param>
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
#if NETCOREAPP3_1_OR_GREATER
            this.CanExecuteCallback = parameter => !this.ExpectsOwnerWindow || (this.ExpectsOwnerWindow && parameter is Window);
#else
            this.CanExecuteCallback = parameter => !this.ExpectsOwnerWindow || (this.ExpectsOwnerWindow && parameter is Visual);
#endif
            this.ExecuteCallback = parameter =>
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = this.Filter;
                dialog.FileName = this.SelectedFile;
#if NETCOREAPP3_1_OR_GREATER
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
    }
}
