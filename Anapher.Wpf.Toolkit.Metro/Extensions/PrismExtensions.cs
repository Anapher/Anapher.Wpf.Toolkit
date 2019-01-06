using System;
using System.Reflection;
using System.Windows;
using Anapher.Wpf.Toolkit.Metro.Services;
using Anapher.Wpf.Toolkit.Prism;
using Anapher.Wpf.Toolkit.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Unity;

namespace Anapher.Wpf.Toolkit.Metro.Extensions
{
    public static class PrismExtensions
    {
        public static void RegisterWindowServices(this IContainerRegistry containerRegistry, Assembly viewsAssembly, Assembly viewModelsAssembly)
        {
            containerRegistry.RegisterSingleton<IShellWindowFactory, ShellWindowFactory>();

            var viewModelLocator = new ViewModelResolver(viewModelsAssembly, viewsAssembly);
            containerRegistry.RegisterInstance<IViewModelResolver>(viewModelLocator);
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewModelLocator.ResolveViewModelType);
        }

        public static void RegisterShell(this IUnityContainer container, Window shellWindow)
        {
            if (!(shellWindow is IWindow))
                throw new ArgumentException("The main window must implement IWindow.");

            container.RegisterInstance(typeof(IWindow), shellWindow);

            container.RegisterSingleton<WindowService>();
            container.RegisterType<IWindowService, WindowService>();
            container.RegisterType<IWindowInteractionService, WindowService>();
        }
    }
}