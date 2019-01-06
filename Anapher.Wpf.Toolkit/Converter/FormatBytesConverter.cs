﻿using System;
using System.Globalization;
using System.Windows.Data;
using Anapher.Wpf.Toolkit.Utilities;

namespace Anapher.Wpf.Toolkit.Converter
{
    /// <summary>
    ///     Format the given bytes as a readable string
    /// </summary>
    [ValueConversion(typeof (long), typeof (string))]
    public class FormatBytesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BytesToString((long) value.ToDouble());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

		/// <summary>
		///     Format the given bytes
		/// </summary>
		/// <param name="byteCount">The bytes to format</param>
		/// <returns>Return readable string</returns>
		//http://www.somacon.com/p576.php
		public static string BytesToString(long byteCount)
        {
	        // Get absolute value
	        long absolute = byteCount < 0 ? -1 * byteCount : byteCount;
	        // Determine the suffix and readable value
	        string suffix;
	        double readable;
	        if (absolute >= 0x1000000000000000) // Exabyte
	        {
		        suffix = "EiB";
		        readable = byteCount >> 50;
	        }
	        else if (absolute >= 0x4000000000000) // Petabyte
	        {
		        suffix = "PiB";
		        readable = byteCount >> 40;
	        }
	        else if (absolute >= 0x10000000000) // Terabyte
	        {
		        suffix = "TiB";
		        readable = byteCount >> 30;
	        }
	        else if (absolute >= 0x40000000) // Gigabyte
	        {
		        suffix = "GiB";
		        readable = byteCount >> 20;
	        }
	        else if (absolute >= 0x100000) // Megabyte
	        {
		        suffix = "MiB";
		        readable = byteCount >> 10;
	        }
	        else if (absolute >= 0x400) // Kilobyte
	        {
		        suffix = "KiB";
		        readable = byteCount;
	        }
	        else
	        {
		        return byteCount.ToString("0 B"); // Byte
	        }
	        // Divide by 1024 to get fractional value
	        readable = readable / 1024;
	        // Return formatted number with suffix
	        return readable.ToString("0.## ") + suffix;
		}
    }
}