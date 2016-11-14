namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Windows.Input;

    public class GenericDelegateCommand<T> : ICommand
    {
        private Func<T, bool> canExecuteCallback;
        private Action<T> executeCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public GenericDelegateCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public GenericDelegateCommand(Func<T, bool> canExecuteCallback, Action<T> executeCallback)
        {
            this.canExecuteCallback = canExecuteCallback;
            this.executeCallback = executeCallback;
        }

        #region Implementation of ICommand

        public Func<T, bool> CanExecuteCallback
        {
            get
            {
                return this.canExecuteCallback;
            }

            set
            {
                this.canExecuteCallback = value;
            }
        }

        public Action<T> ExecuteCallback
        {
            get
            {
                return this.executeCallback;
            }

            set
            {
                this.executeCallback = value;
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public virtual bool CanExecute(object parameter)
        {
            return this.canExecuteCallback((T)parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public virtual void Execute(object parameter)
        {
            this.executeCallback((T)parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public virtual event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion
    }
}