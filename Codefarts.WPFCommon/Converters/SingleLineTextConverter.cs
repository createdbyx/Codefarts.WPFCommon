// <copyright file="SingleLineTextConverter.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.WPFCommon.Converters
{
    using System;
    using System.Windows.Data;

    public class SingleLineTextConverter : IValueConverter
    {
        public bool Crop
        {
            get; set;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var someString = (string)value;
            var indexOfNewLine = someString.IndexOf(Environment.NewLine);
            someString = this.Crop && indexOfNewLine > -1 ? someString.Remove(indexOfNewLine) : someString.Replace(Environment.NewLine, " ");
            return someString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
