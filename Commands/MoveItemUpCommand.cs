namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Collections;

    public class MoveItemUpCommand : MoveListItemCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveItemUpCommand"/> class.
        /// </summary>
        public MoveItemUpCommand()
        {
        }

        public MoveItemUpCommand(IList items)
            : base(items)
        {
        }

        public MoveItemUpCommand(Action completed)
            : base(completed)
        {
        }

        public MoveItemUpCommand(IList items, Action completed)
            : base(items, completed)
        {
        }

        public override bool CanExecute(object parameter)
        {
            if (!base.CanExecute(parameter))
            {
                return false;
            }

            var index = this.Items.IndexOf(parameter);
            return this.Items.Count > 1 && index > 0;
        }

        public override void DoMove(int index)
        {
            var tempItem = this.Items[index];
            this.Items[index] = this.Items[index - 1];
            this.Items[index - 1] = tempItem;
        }
    }
}
