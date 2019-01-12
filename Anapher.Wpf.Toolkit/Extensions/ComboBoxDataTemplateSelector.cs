﻿using System.Windows;
using System.Windows.Controls;
using Anapher.Wpf.Toolkit.Utilities;

namespace Anapher.Wpf.Toolkit.Extensions
{
    public class ComboBoxDataTemplateSelector : DataTemplateSelector
    {
        public static DependencyProperty SelectedTemplateProperty =
            DependencyProperty.RegisterAttached("SelectedTemplate", typeof(DataTemplate),
                typeof(ComboBoxDataTemplateSelector), new UIPropertyMetadata(null));

        public static DependencyProperty DropDownTemplateProperty =
            DependencyProperty.RegisterAttached("DropDownTemplate", typeof(DataTemplate),
                typeof(ComboBoxDataTemplateSelector), new UIPropertyMetadata(null));

        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static DataTemplate GetSelectedTemplate(ComboBox obj) =>
            (DataTemplate) obj.GetValue(SelectedTemplateProperty);

        public static void SetSelectedTemplate(ComboBox obj, DataTemplate value)
        {
            obj.SetValue(SelectedTemplateProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static DataTemplate GetDropDownTemplate(ComboBox obj) =>
            (DataTemplate) obj.GetValue(DropDownTemplateProperty);

        public static void SetDropDownTemplate(ComboBox obj, DataTemplate value)
        {
            obj.SetValue(DropDownTemplateProperty, value);
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ComboBox parentComboBox;
            var comboBoxItem = container.GetVisualParent<ComboBoxItem>();
            if (comboBoxItem == null)
            {
                parentComboBox = container.GetVisualParent<ComboBox>();
                return GetSelectedTemplate(parentComboBox);
            }

            parentComboBox = ItemsControl.ItemsControlFromItemContainer(comboBoxItem) as ComboBox;
            return GetDropDownTemplate(parentComboBox);
        }
    }
}