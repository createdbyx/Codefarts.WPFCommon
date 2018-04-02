namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.Collections;

    public class MoveItemDownCommand : MoveListItemCommand
    {
        public MoveItemDownCommand(Action completed) : base(completed)
        {
        }

        public MoveItemDownCommand()
        {
        }

        public MoveItemDownCommand(IList items) : base(items)
        {
        }

        public MoveItemDownCommand(IList items, Action completed) : base(items, completed)
        {
        }

        public override void DoMove(int index)
        {
            if (this.Items.Count < 2 || index == this.Items.Count - 1)
            {
                return;
            }

            var tempItem = this.Items[index + 1];
            this.Items[index + 1] = this.Items[index];
            this.Items[index] = tempItem;
        }
    }
}