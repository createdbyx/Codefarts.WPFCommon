// <copyright file="OpenFileCommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>


using System.Collections.Generic;

namespace Codefarts.WPFCommon.Commands
{
    using System;
#if NETCOREAPP3_1 || NET5_0_OR_GREATER
    using Microsoft.Win32;
    using System.Windows;
#else
    using System.Windows.Forms;
#endif

    public class OpenFileCommand : DelegateCommand
    {
        private bool multiSelect;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileCommand"/> class.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public OpenFileCommand(string filter)
        {
            this.Filter = filter;
        }

        public bool MultiSelect
        {
            get
            {
                return this.multiSelect;
            }

            set
            {
                var currentValue = this.multiSelect;
                if (currentValue != value)
                {
                    this.multiSelect = value;
                    this.NotifyOfPropertyChange(() => this.MultiSelect);
                }
            }
        }

        public Action<IEnumerable<string>> MultipleFilesSelected
        {
            get; set;
        }

        public Action<string> FileSelected
        {
            get; set;
        }

        public string SelectedFile
        {
            get; set;
        }

        public IEnumerable<string> SelectedFiles
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
        /// Initializes a new instance of the <see cref="OpenFileCommand"/> class.
        /// </summary>
        public OpenFileCommand(bool expectsOwnerWindow)
            : this()
        {
            this.ExpectsOwnerWindow = expectsOwnerWindow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileCommand"/> class.
        /// </summary>
        /// <param name="fileSelectedCallback">The file selected callback that will be called when a selection is made.</param>
        public OpenFileCommand(Action<IEnumerable<string>> fileSelectedCallback)
            : this()
        {
            this.MultipleFilesSelected = fileSelectedCallback;
            this.MultiSelect = true;
        }

        public OpenFileCommand(Action<string> fileSelectedCallback)
            : this()
        {
            this.FileSelected = fileSelectedCallback;
            this.MultiSelect = false;
        }

        public OpenFileCommand(string file, Action<string> pathSelectedCallback)
            : this(pathSelectedCallback)
        {
            this.SelectedFile = file;
            this.MultiSelect = false;
        }

        public OpenFileCommand(string file, string filter, Action<IEnumerable<string>> pathSelectedCallback)
            : this(pathSelectedCallback)
        {
            this.SelectedFile = file;
            this.Filter = filter;
            this.MultiSelect = true;
        }

        public OpenFileCommand(string file, string filter, Action<string> pathSelectedCallback)
            : this(pathSelectedCallback)
        {
            this.SelectedFile = file;
            this.Filter = filter;
            this.MultiSelect = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileCommand"/> class.
        /// </summary>
        public OpenFileCommand()
        {
#if NETCOREAPP3_1 || NET5_0_OR_GREATER
            this.CanExecuteCallback = parameter => !this.ExpectsOwnerWindow || (this.ExpectsOwnerWindow && parameter is Window);
#else
            this.CanExecuteCallback = parameter => !this.ExpectsOwnerWindow || (this.ExpectsOwnerWindow && parameter is Visual);
#endif
            this.ExecuteCallback = parameter =>
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = this.Filter;
                dialog.FileName = this.SelectedFile;
                dialog.Multiselect = this.multiSelect;

#if NETCOREAPP3_1 || NET5_0_OR_GREATER
                var window = parameter as Window;
                var result = !this.ExpectsOwnerWindow ? dialog.ShowDialog() : dialog.ShowDialog(window);
                if (result.HasValue && result.Value)
#else
                var window = parameter as Visual;
                var result = !this.ExpectsOwnerWindow ? dialog.ShowDialog() : dialog.ShowDialog(HelpersFunctions.GetIWin32Window(window));
                if (result == DialogResult.OK)
#endif
                {
                    if (dialog.Multiselect)
                    {
                        this.SelectedFiles = dialog.FileNames;
                        var action = this.MultipleFilesSelected;
                        if (action != null)
                        {
                            action(dialog.FileNames);
                        }
                    }
                    else
                    {
                        this.SelectedFile = dialog.FileName;
                        var action = this.FileSelected;
                        if (action != null)
                        {
                            action(dialog.FileName);
                        }
                    }
                }
            };
        }

        public OpenFileCommand(Action<IEnumerable<string>> pathSelectedCallback, string filter, bool expectsOwnerWindow)
            : this(pathSelectedCallback)
        {
            this.Filter = filter;
            this.ExpectsOwnerWindow = expectsOwnerWindow;
            this.MultiSelect = true;
        }

        public OpenFileCommand(Action<string> pathSelectedCallback, string filter, bool expectsOwnerWindow)
            : this(pathSelectedCallback)
        {
            this.Filter = filter;
            this.ExpectsOwnerWindow = expectsOwnerWindow;
            this.MultiSelect = false;
        }

        public OpenFileCommand(Action<IEnumerable<string>> pathSelectedCallback, string filter)
            : this(pathSelectedCallback)
        {
            this.Filter = filter;
            this.MultiSelect = true;
        }

        public OpenFileCommand(Action<string> pathSelectedCallback, string filter)
            : this(pathSelectedCallback)
        {
            this.Filter = filter;
            this.MultiSelect = false;
        }

    }
}