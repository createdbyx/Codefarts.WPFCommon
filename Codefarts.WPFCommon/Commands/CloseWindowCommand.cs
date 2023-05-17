namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class CloseWindowCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseWindowCommand"/> class.
        /// </summary>
        public CloseWindowCommand()
        {
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

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public virtual bool CanExecute(object parameter)
        {
            return parameter != null && parameter is Window;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public virtual void Execute(object parameter)
        {
            var window = parameter as Window;
            if (window != null)
            {
                window.Close();
            }
        }
    }
}