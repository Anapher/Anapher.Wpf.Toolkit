﻿using System;
using System.Windows.Data;

namespace Anapher.Wpf.Toolkit.Converter
{
    /// <summary>
    ///     Return true if the value equals the parameter
    /// </summary>
    /// <example>
    /// <code>
    ///     &lt;RadioButton IsChecked="{Binding TestValue,
    ///     Converter={StaticResource EnumBooleanConverter},
    ///     ConverterParameter={x:Static local:Enum.Value}}" />
    /// </code>
    /// </example>
    [ValueConversion(typeof (Enum), typeof (bool), ParameterType = typeof (object))]
    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Equals(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return Equals(value, true) ? parameter : Binding.DoNothing;
        }
    }
}