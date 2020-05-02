namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class MultiCommand : ICommand
    {
        private List<ICommand> commands;

        public MultiCommand()
        {
            this.commands = new List<ICommand>();
        }

        public List<ICommand> Commands
        {
            get
            {
                return this.commands;
            }
        }

        public MultiCommand(IEnumerable<ICommand> commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            this.commands = new List<ICommand>(commands);
        }

        public bool CanExecute(object parameter)
        {
            return this.commands.TrueForAll(x => x.CanExecute(parameter));
        }

        public void Execute(object parameter)
        {
            this.commands.ForEach(x => x.Execute(parameter));
        }

        public event EventHandler CanExecuteChanged
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
    }
}