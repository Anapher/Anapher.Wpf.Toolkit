using Anapher.Wpf.Toolkit.Metro.Views;
using Anapher.Wpf.Toolkit.Prism;
using Anapher.Wpf.Toolkit.Windows;

namespace Anapher.Wpf.Toolkit.Metro.Services
{
    public class ShellWindowFactory : IShellWindowFactory
    {
        public IShellWindow Create() => new MetroShellWindow();
    }
}