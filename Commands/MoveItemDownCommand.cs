using System;

namespace Codefarts.WPFCommon.Commands
{
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
            this.Items.MoveDown(index);
        }
    }
}