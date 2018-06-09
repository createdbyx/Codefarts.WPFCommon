namespace Codefarts.WPFCommon.Behaviours
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public static class ItemsControlAttachedProperties
    {
        public static readonly DependencyProperty ScrollToTopOnItemsSourceChangeProperty =
            DependencyProperty.RegisterAttached(
                "ScrollToTopOnItemsSourceChange",
                typeof(bool),
                typeof(ItemsControlAttachedProperties),
                new UIPropertyMetadata(false, OnScrollToTopOnItemsSourceChangePropertyChanged));

        public static bool GetScrollToTopOnItemsSourceChange(DependencyObject obj)
        {
            return (bool)obj.GetValue(ScrollToTopOnItemsSourceChangeProperty);
        }

        public static void SetScrollToTopOnItemsSourceChange(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollToTopOnItemsSourceChangeProperty, value);
        }

        static void OnScrollToTopOnItemsSourceChangePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var itemsControl = obj as ItemsControl;
            if (itemsControl == null)
            {
                throw new Exception("ScrollToTopOnItemsSourceChange Property must be attached to an ItemsControl based control.");
            }

            var descriptor =
                DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(ItemsControl));
            if (descriptor != null)
            {
                if ((bool)e.NewValue)
                {
                    descriptor.AddValueChanged(itemsControl, ItemsSourceChanged);
                }
                else
                {
                    descriptor.RemoveValueChanged(itemsControl, ItemsSourceChanged);
                }
            }
        }

        static void ItemsSourceChanged(object sender, EventArgs e)
        {
            var itemsControl = sender as ItemsControl;
            DoScrollToTop(itemsControl);

            var collection = itemsControl.ItemsSource as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged += (o, args) => DoScrollToTop(itemsControl);
            }
        }

        static void DoScrollToTop(ItemsControl itemsControl)
        {
            EventHandler eventHandler = null;
            eventHandler =
                (sender, args) =>
                {
                    if (itemsControl.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                    {
                        var scrollViewer = GetVisualChild<ScrollViewer>(itemsControl);
                        scrollViewer.ScrollToTop();
                        itemsControl.ItemContainerGenerator.StatusChanged -= eventHandler;
                    }
                };

            itemsControl.ItemContainerGenerator.StatusChanged += eventHandler;
        }

        static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            var child = default(T);
            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);
                if (child != null)
                {
                    break;
                }
            }

            return child;
        }
    }
}