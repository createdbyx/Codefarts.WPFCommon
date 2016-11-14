namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Collections;
    using System.Windows.Input;

    public abstract class MoveListItemCommand : ICommand
    {
        protected MoveListItemCommand(Action completed)
        {
            Completed = completed;
        }

        protected MoveListItemCommand(IList items, Action completed)
        {
            Items = items;
            Completed = completed;
        }

        public IList Items { get; set; }
        public Action Completed { get; set; }

        public MoveListItemCommand()
        {
        }

        public MoveListItemCommand(IList items)
        {
            Items = items;
        }

        public bool CanExecute(object parameter)
        {
            return this.Items != null;
        }

        public void Execute(object parameter)
        {
            var index = this.Items.IndexOf(parameter);
            if (index != -1)
            {
                this.DoMove(index);
                if (this.Completed != null)
                {
                    this.Completed();
                }
            }
        }

        public abstract void DoMove(int index);

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
    }
}