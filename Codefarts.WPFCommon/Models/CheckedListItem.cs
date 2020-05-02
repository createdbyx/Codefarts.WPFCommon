namespace Codefarts.WPFCommon.Models
{
    using System.ComponentModel;

    public class CheckedListItem<T> : INotifyPropertyChanged where T : class
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isChecked;
        private T item;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedListItem{T}"/> class.
        /// </summary>
        public CheckedListItem()
        {
        }

        public CheckedListItem(T item, bool isChecked = false)
        {
            this.item = item;
            this.isChecked = isChecked;
        }

        public T Item
        {
            get
            {
                return this.item;
            }

            set
            {
                var oldValue = this.item;
                this.item = value;
                var handler = this.PropertyChanged;
                if (oldValue != this.item && handler != null)
                {
                    handler(this, new PropertyChangedEventArgs("Item"));
                }
            }
        }


        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                var oldValue = this.isChecked;
                this.isChecked = value;
                var handler = this.PropertyChanged;
                if (oldValue != this.isChecked && handler != null)
                {
                    handler(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }
    }
}
