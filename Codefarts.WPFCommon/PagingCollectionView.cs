namespace Codefarts.WPFCommon
{
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Data;

    public class PagingCollectionView : CollectionView
    {
        private readonly IList innerList;
        private int itemsPerPage;

        private int currentPage = 1;

        public PagingCollectionView(IList innerList, int itemsPerPage)
            : base(innerList)
        {
            this.innerList = innerList;
            this.itemsPerPage = itemsPerPage;
        }

        public override int Count
        {
            get
            {
                if (this.innerList.Count == 0)
                {
                    return 0;
                }

                if (this.currentPage < this.PageCount) // page 1..n-1
                {
                    return this.itemsPerPage;
                }

                var itemsLeft = this.innerList.Count % this.itemsPerPage;
                if (itemsLeft == 0)
                {
                    return this.itemsPerPage; // exactly itemsPerPage left
                }

                // return the remaining items
                return itemsLeft;
            }
        }

        public int CurrentPage
        {
            get
            {
                return this.currentPage;
            }

            set
            {
                this.currentPage = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.CurrentPage)));
                this.Refresh();
            }
        }

        public int ItemsPerPage
        {
            get
            {
                return this.itemsPerPage;
            }

            set
            {
                this.itemsPerPage = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ItemsPerPage)));
                this.Refresh();
            }
        }

        public int PageCount
        {
            get
            {
                return (this.innerList.Count + this.itemsPerPage - 1) / this.itemsPerPage;
            }
        }

        private int EndIndex
        {
            get
            {
                var end = this.currentPage * this.itemsPerPage - 1;
                return (end > this.innerList.Count) ? this.innerList.Count : end;
            }
        }

        private int StartIndex
        {
            get
            {
                return (this.currentPage - 1) * this.itemsPerPage;
            }
        }

        public override object GetItemAt(int index)
        {
            var offset = index % this.itemsPerPage;
            return this.innerList[this.StartIndex + offset];
        }

        public void MoveToNextPage()
        {
            if (this.currentPage < this.PageCount)
            {
                this.CurrentPage += 1;
            }

            this.Refresh();
        }

        public void MoveToPreviousPage()
        {
            if (this.currentPage > 1)
            {
                this.CurrentPage -= 1;
            }

            this.Refresh();
        }
    }
}