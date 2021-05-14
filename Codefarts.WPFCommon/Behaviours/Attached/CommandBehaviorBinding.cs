using System.ComponentModel;

namespace AttachedCommandBehavior
{
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Defines the command behavior binding.
    /// </summary>
    public class CommandBehaviorBinding : IDisposable
    {
        // stores the strategy of how to execute the event handler
        Action<object> action;
        ICommand command;
        IExecutionStrategy strategy;
        bool disposed;

        /// <summary>
        /// Get the owner of the CommandBinding ex: a Button
        /// This property can only be set from the BindEvent Method.
        /// </summary>
        public DependencyObject Owner
        {
            get; private set;
        }

        /// <summary>
        /// The event name to hook up to
        /// This property can only be set from the BindEvent Method.
        /// </summary>
        public string EventName
        {
            get; private set;
        }

        /// <summary>
        /// The event info of the event.
        /// </summary>
        public EventInfo Event
        {
            get; private set;
        }

        /// <summary>
        /// Gets the EventHandler for the binding with the event.
        /// </summary>
        public Delegate EventHandler
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets a CommandParameter.
        /// </summary>
        public object CommandParameter
        {
            get; set;
        }

        /// <summary>
        /// The command to execute when the specified event is raised.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return this.command;
            }

            set
            {
                this.command = value;

                // set the execution strategy to execute the command
                this.strategy = new CommandExecutionStrategy { Behavior = this };
            }
        }

        /// <summary>
        /// Gets or sets the Action.
        /// </summary>
        public Action<object> Action
        {
            get
            {
                return this.action;
            }

            set
            {
                this.action = value;

                // set the execution strategy to execute the action
                this.strategy = new ActionExecutionStrategy { Behavior = this };
            }
        }

        // Creates an EventHandler on runtime and registers that handler to the Event specified
        public void BindEvent(DependencyObject owner, string eventName)
        {
            this.EventName = eventName;
            this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            this.Event = owner.GetType().GetEvent(this.EventName, BindingFlags.Public | BindingFlags.Instance);
            if (this.Event == null)
            {
                throw new InvalidOperationException(string.Format("Could not resolve event name {0}", this.EventName));
            }

            // Create an event handler for the event that will call the ExecuteCommand method
            this.EventHandler = EventHandlerGenerator.CreateDelegate(
                this.Event.EventHandlerType, typeof(CommandBehaviorBinding).GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance), this);

            // Register the handler to the Event
            this.Event.AddEventHandler(this.Owner, this.EventHandler);
        }

        /// <summary>
        /// Executes the strategy.
        /// </summary>
        public void Execute()
        {
            var application = Application.Current;
            if (!DesignerProperties.GetIsInDesignMode(application.MainWindow ?? throw new InvalidOperationException()))
            {
                this.strategy.Execute(this.CommandParameter);
            }
        }

        /// <summary>
        /// De-register the EventHandler from the Event.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                this.Event.RemoveEventHandler(this.Owner, this.EventHandler);
                this.disposed = true;
            }
        }
    }
}
