namespace Codefarts.WPFCommon.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class IntegerDividerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterValue = parameter != null ? System.Convert.ToInt32(parameter) : 0;

            var returnValue = System.Convert.ToInt32(value) / parameterValue;
            return System.Convert.ChangeType(returnValue, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}