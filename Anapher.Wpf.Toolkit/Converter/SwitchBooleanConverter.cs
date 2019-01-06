using System;
using System.Globalization;
using System.Windows.Data;

namespace Anapher.Wpf.Toolkit.Converter
{
    /// <summary>
    ///     Switch the value of a boolean (true to false, false to true)
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class SwitchBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool) value;
        }
    }
}