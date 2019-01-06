using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Anapher.Wpf.Toolkit.Extensions
{
    /// <summary>
    ///     Fill an <see cref="ItemsControl" /> with the values of an Enum
    /// </summary>
    /// <example>
    /// <code>
    ///     &lt;ComboBox
    ///     ItemsSource="{Binding Source={my:Enumeration {x:Type my:Status}}}"
    ///     DisplayMemberPath="Description"
    ///     SelectedValue="{Binding CurrentStatus}"
    ///     SelectedValuePath="Value"  />
    /// </code>
    /// </example>
    public class EnumerationExtension : MarkupExtension
    {
        private Type _enumType;

        public EnumerationExtension(Type enumType)
        {
			EnumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
        }

        public Type EnumType
        {
            get => _enumType;
	        private set
            {
                if (_enumType == value)
                    return;

                var enumType = Nullable.GetUnderlyingType(value) ?? value;

                if (enumType.IsEnum == false)
                    throw new ArgumentException("Type must be an Enum.");

                _enumType = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
	        return Enum.GetValues(EnumType).Cast<object>()
		        .Select(x => new EnumerationMember {Value = x, Description = GetDescription(x)}).ToArray();
        }

	    private string GetDescription(object enumValue)
	    {
		    var descriptionAttribute = EnumType
			    .GetField(enumValue.ToString()).GetCustomAttribute<DescriptionAttribute>();

		    return descriptionAttribute != null
			    ? descriptionAttribute.Description
			    : enumValue.ToString();
	    }

	    public class EnumerationMember
        {
            public string Description { get; set; }
            public object Value { get; set; }
        }
    }
}