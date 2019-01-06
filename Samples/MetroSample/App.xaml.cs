using System.Windows;
using Anapher.Wpf.Toolkit.Metro.Extensions;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;

namespace MetroSample
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterWindowServices(GetType().Assembly, GetType().Assembly);
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            Container.GetContainer().RegisterShell(shell);
            ViewModelLocator.SetAutoWireViewModel(shell, true);
        }

        protected override Window CreateShell() => new MainWindow();
    }
}