namespace Codefarts.WPFCommon.Behaviours
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Provides attached behaviours for <see cref="ItemsControl"/> for setting up filtering within xaml.
    /// </summary>
    public static class FilterBehaviours
    {
        /// <summary>
        /// The by property for attaching the filter in xaml.
        /// </summary>
        /// <example>
        /// <![CDATA[<DataGrid local:Filter.By="{Binding Filter} ItemsSource="{Binding Foos}" />]]>
        /// </example>
        public static readonly DependencyProperty ByProperty = DependencyProperty.RegisterAttached(
            "By",
            typeof(Predicate<object>),
            typeof(FilterBehaviours),
            new PropertyMetadata(default(Predicate<object>), OnWithChanged));

        /// <summary>
        /// Sets the by property on a <see cref="ItemsControl"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetBy(ItemsControl element, Predicate<object> value)
        {
            element.SetValue(ByProperty, value);
        }

        /// <summary>
        /// Gets the by property on a <see cref="ItemsControl"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The value of the by property.</returns>
        public static Predicate<object> GetBy(ItemsControl element)
        {
            return (Predicate<object>)element.GetValue(ByProperty);
        }

        /// <summary>Called when [with changed].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnWithChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var items = (ItemsControl)sender;
            if (items.Items.CanFilter)
            {
                items.Items.Filter = (Predicate<object>)e.NewValue;
            }
        }
    }
}