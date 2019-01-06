using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Anapher.Wpf.Toolkit.Utilities;

namespace Anapher.Wpf.Toolkit.Converter
{
    /// <summary>
    ///     Get a specific bitmap with the given size
    /// </summary>
    [ValueConversion(typeof (ImageSource), typeof (ImageSource), ParameterType = typeof (double))]
    public class IconSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
	        if (!(value is BitmapFrame bitmapFrame))
                return value;

            var decoder = bitmapFrame.Decoder;
            var desiredSize = parameter.ToDouble();

            var result = decoder.Frames.FirstOrDefault(f => f.Width == desiredSize) ??
                         decoder.Frames.OrderBy(f => f.Width).First();

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}