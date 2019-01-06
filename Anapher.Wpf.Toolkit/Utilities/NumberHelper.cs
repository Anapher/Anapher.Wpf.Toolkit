using System;

namespace Anapher.Wpf.Toolkit.Utilities
{
	/// <summary>
	///     Helper for parsing an <see cref="object" /> to a number
	/// </summary>
	public static class NumberHelper
	{
		/// <summary>
		///     Try to cast the object to any integer value type
		/// </summary>
		/// <param name="value">The value to cast</param>
		/// <param name="tryParse">True if parsing should be tried</param>
		/// <returns>Return null if it could not be casted to any integer value type</returns>
		public static long? ToLong(this object value, bool tryParse)
		{
			if (value is byte b)
				return b;
			if (value is sbyte @sbyte)
				return @sbyte;
			if (value is short s)
				return s;
			if (value is ushort @ushort)
				return @ushort;
			if (value is int i)
				return i;
			if (value is uint u)
				return u;
			if (value is long l)
				return l;

			if (tryParse && long.TryParse(value.ToString(), out var longValue))
				return longValue;

			return null;
		}

		/// <summary>
		///     Try to cast the object to any integer value type
		/// </summary>
		/// <param name="value">The value to cast</param>
		/// <param name="tryParse">True if parsing should be tried</param>
		/// <returns>Return null if it could not be casted to any integer value type</returns>
		public static int? ToInteger(this object value, bool tryParse)
		{
			if (value is byte b)
				return b;
			if (value is sbyte @sbyte)
				return @sbyte;
			if (value is short s)
				return s;
			if (value is ushort @ushort)
				return @ushort;
			if (value is int i)
				return i;

			if (tryParse && int.TryParse(value.ToString(), out var integerValue))
				return integerValue;

			return null;
		}

		/// <summary>
		///     Try to cast the object to any floating value type
		/// </summary>
		/// <param name="value">The value to cast</param>
		/// <param name="tryParse">True if parsing should be tried</param>
		/// <returns>Return null if it could not be casted to any integer value type</returns>
		public static double? ToDouble(this object value, bool tryParse)
		{
			var integerValue = ToLong(value, false);
			if (integerValue != null)
				return integerValue;

			if (value is double d)
				return d;
			if (value is float f)
				return f;
			if (value is decimal d1)
				return (double) d1;

			if (tryParse && double.TryParse(value.ToString(), out var doubleValue))
				return doubleValue;

			return null;
		}

		/// <summary>
		///     Try to cast the object to any integer value type. Throw exception if that was not possible
		/// </summary>
		/// <param name="value">The value to cast</param>
		/// <returns>Return the casted value</returns>
		/// <example cref="ArgumentException">Thrown when the <see cref="value" /> could not be converted</example>
		public static int ToInteger(this object value)
		{
			return value.ToInteger(true) ?? throw new ArgumentException(nameof(value));
		}

		/// <summary>
		///     Try to cast the object to any integer value type. Throw exception if that was not possible
		/// </summary>
		/// <param name="value">The value to cast</param>
		/// <returns>Return the casted value</returns>
		/// <example cref="ArgumentException">Thrown when the <see cref="value" /> could not be converted</example>
		public static long ToLong(this object value)
		{
			return value.ToLong(true) ?? throw new ArgumentException(nameof(value));
		}

		/// <summary>
		///     Try to cast the object to any floating value type. Throw exception if that was not possible
		/// </summary>
		/// <param name="value">The value to cast</param>
		/// <returns>Return the casted value</returns>
		/// <example cref="ArgumentException">Thrown when the <see cref="value" /> could not be converted</example>
		public static double ToDouble(this object value)
		{
			return value.ToDouble(true) ?? throw new ArgumentException(nameof(value));
		}
	}
}