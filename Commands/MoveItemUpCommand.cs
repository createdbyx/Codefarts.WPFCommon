using System;

namespace Codefarts.WPFCommon.Commands
{
    using System.Collections;

    public class MoveItemUpCommand : MoveListItemCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveItemUpCommand"/> class.
        /// </summary>
        public MoveItemUpCommand()
        {
        }

        public MoveItemUpCommand(IList items) : base(items)
        {
        }

        public MoveItemUpCommand(Action completed) : base(completed)
        {
        }

        public MoveItemUpCommand(IList items, Action completed) : base(items, completed)
        {
        }

        public override void DoMove(int index)
        {
            this.Items.MoveUp(index);
        }
    }
}
