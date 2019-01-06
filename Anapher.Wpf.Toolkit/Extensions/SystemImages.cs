using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Anapher.Wpf.Toolkit.Native;

namespace Anapher.Wpf.Toolkit.Extensions
{
	public static class SystemImages
	{
		private static BitmapSource _uacIcon;

		public static BitmapSource UacIcon
		{
			get
			{
				if (_uacIcon == null)
				{
					var sii = new SHSTOCKICONINFO {cbSize = (uint) Marshal.SizeOf(typeof(SHSTOCKICONINFO))};

					Marshal.ThrowExceptionForHR(NativeMethods.SHGetStockIconInfo(SHSTOCKICONID.SIID_SHIELD,
						SHGSI.SHGSI_ICON | SHGSI.SHGSI_SMALLICON,
						ref sii));

					try
					{
						_uacIcon = Imaging.CreateBitmapSourceFromHIcon(
							sii.hIcon,
							Int32Rect.Empty,
							BitmapSizeOptions.FromEmptyOptions());
					}
					finally
					{
						NativeMethods.DestroyIcon(sii.hIcon);
					}
				}

				return _uacIcon;
			}
		}
	}
}