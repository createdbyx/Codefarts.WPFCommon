namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Collections;
    using System.Windows.Input;

    public abstract class MoveListItemCommand : ICommand
    {
        protected MoveListItemCommand(Action completed)
        {
            this.Completed = completed;
        }

        protected MoveListItemCommand(IList items, Action completed)
        {
            this.Items = items;
            this.Completed = completed;
        }

        public IList Items { get; set; }

        public Action Completed { get; set; }

        public MoveListItemCommand()
        {
        }

        public MoveListItemCommand(IList items)
        {
            this.Items = items;
        }

        public virtual bool CanExecute(object parameter)
        {
            if (this.Items == null || parameter == null)
            {
                return false;
            }

            var index = this.Items.IndexOf(parameter);
            return index != -1;
        }

        public void Execute(object parameter)
        {
            var index = this.Items.IndexOf(parameter);
            this.DoMove(index);
            if (this.Completed != null)
            {
                this.Completed();
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