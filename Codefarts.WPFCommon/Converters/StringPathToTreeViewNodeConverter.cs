namespace Codefarts.WPFCommon.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Converts a path string into a <see cref="TreeViewItem"/>.
    /// </summary>
    public class StringPathToTreeViewNodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = System.Convert.ToString(value);
            path = string.IsNullOrWhiteSpace(path) ? string.Empty : path;


            var item = new TreeViewItem();
            item.Header = path;
            return item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}