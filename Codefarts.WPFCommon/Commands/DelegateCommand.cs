namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Input;
    using System.Xml.Serialization;

    public class DelegateCommand : INotifyPropertyChanged, ICommand
    {
        private Func<object, bool> canExecuteCallback;
        private Action<object> executeCallback;

        private bool isNotifying;
        public event EventHandler Initialize;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        public DelegateCommand()
        {
            this.isNotifying = true;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Enables/Disables property change notification.
        /// Virtualized in order to help with document oriented view models.
        /// </summary>
        [XmlIgnore]
        public virtual bool IsNotifying
        {
            get
            {
                return this.isNotifying;
            }

            set
            {
                var currentValue = this.isNotifying;
                if (currentValue != value)
                {
                    this.isNotifying = value;
                    this.NotifyOfPropertyChange(() => this.IsNotifying);
                }
            }
        }

        /// <summary>
        /// Raises a change notification indicating that all bindings should be refreshed.
        /// </summary>
        public virtual void Refresh()
        {
            this.NotifyOfPropertyChange(string.Empty);
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
//#if NET || SILVERLIGHT || UNITY_5   
        public virtual void NotifyOfPropertyChange(string propertyName)
        {
            //#else
            //    public virtual void NotifyOfPropertyChange([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
            //      {
            //#endif
            var handler = this.PropertyChanged;
            if (this.IsNotifying && handler != null)
            {
                this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <typeparam name = "TProperty">The type of the property.</typeparam>
        /// <param name = "property">The property expression.</param>
        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            this.NotifyOfPropertyChange(this.GetMemberInfo(property).Name);
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged" /> event directly.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private MemberInfo GetMemberInfo(Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DelegateCommand(Func<object, bool> canExecuteCallback, Action<object> executeCallback)
            : this()
        {
            this.canExecuteCallback = canExecuteCallback;
            this.executeCallback = executeCallback;
        }

        public Func<object, bool> CanExecuteCallback
        {
            get
            {
                return this.canExecuteCallback;
            }

            set
            {
                this.canExecuteCallback = value;
            }
        }

        public Action<object> ExecuteCallback
        {
            get
            {
                return this.executeCallback;
            }

            set
            {
                this.executeCallback = value;
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
            this.OnInitialize();
            var callback = this.canExecuteCallback;
            if (callback == null)
            {
                return true;
            }

            return callback(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public virtual void Execute(object parameter)
        {
            var callback = this.executeCallback;
            if (callback != null)
            {
                callback(parameter);
            }
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

        protected virtual void OnInitialize()
        {
            var handler = this.Initialize;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public static DelegateCommand Execute(Action<object> callback)
        {
            return new DelegateCommand(null, callback);
        }
    }
}