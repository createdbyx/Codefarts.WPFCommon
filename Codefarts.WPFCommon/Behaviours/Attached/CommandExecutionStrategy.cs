namespace AttachedCommandBehavior
{
    using System;

    /// <summary>
    /// Executes a command.
    /// </summary>
    public class CommandExecutionStrategy : IExecutionStrategy
    {
        /// <summary>
        /// Gets or sets the Behavior that we execute this strategy.
        /// </summary>
        public CommandBehaviorBinding Behavior
        {
            get; set;
        }

        /// <summary>
        /// Executes the Command that is stored in the CommandProperty of the CommandExecution.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        public void Execute(object parameter)
        {
            var behavior = this.Behavior;
            if (behavior == null)
            {
                throw new InvalidOperationException("Behavior property cannot be null when executing a strategy.");
            }

            if (behavior.Command.CanExecute(behavior.CommandParameter))
            {
                behavior.Command.Execute(behavior.CommandParameter);
            }
        }
    }

}
