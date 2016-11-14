namespace Codefarts.WPFCommon.Commands
{
    using System;
    using System.ComponentModel;            
    using System.Threading.Tasks;
    using System.Windows.Input;

    public abstract class AsyncCommand : ICommand, INotifyPropertyChanged
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler CommandStarting;
        public event EventHandler CommandCompleted;

        private bool isExecuting;

        public bool IsExecuting
        {
            get
            {
                return this.isExecuting;
            }

            private set
            {
                if(this.isExecuting != value)
                {
                    this.isExecuting = value;
                    this.OnPropertyChanged("IsExecuting");
                    var handler = this.CanExecuteChanged;
                    if (handler != null)
                    {
                        handler(this, EventArgs.Empty);
                    }
                }
            }
        }

        protected abstract Task OnExecute(object parameter);

        public void Execute(object parameter)
        {
            try
            {
                this.onRunWorkerStarting();
                Task.Factory.StartNew(
                   async () => await this.OnExecute(parameter))
                    .ContinueWith(
                        task =>
                        {
                            var args = task.Exception != null ? new ExceptionArgs(task.Exception) : EventArgs.Empty;
                            this.onRunWorkerCompleted(args);
                        },
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                this.onRunWorkerCompleted(new ExceptionArgs(ex));
            }
        }

        private void onRunWorkerStarting()
        {
            this.IsExecuting = true;
            var handler = this.CommandStarting;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void onRunWorkerCompleted(EventArgs e)
        {
            this.IsExecuting = false;
            var handler = this.CommandCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual bool CanExecute(object parameter)
        {
            return !this.IsExecuting;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}