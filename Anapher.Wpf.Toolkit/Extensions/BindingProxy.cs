using System.Windows;

namespace Anapher.Wpf.Toolkit.Extensions
{
	/// <summary>
	///     A simple proxy to make bindings to the viewmodel from non visual tree controls (like ContextMenu) possible.
	/// </summary>
	public class BindingProxy : Freezable
	{
		// Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DataProperty =
			DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

		/// <summary>
		///     The data that should be proxied
		/// </summary>
		public object Data
		{
			get => GetValue(DataProperty);
			set => SetValue(DataProperty, value);
		}

		protected override Freezable CreateInstanceCore()
		{
			return new BindingProxy();
		}
	}
}