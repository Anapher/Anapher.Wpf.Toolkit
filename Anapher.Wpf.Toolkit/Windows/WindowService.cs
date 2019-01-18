using System;
using System.Windows;
using Anapher.Wpf.Toolkit.Prism;
using Anapher.Wpf.Toolkit.StatusBar;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using Prism;
using Prism.Regions;
using Unity;
using Unity.Injection;

namespace Anapher.Wpf.Toolkit.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUnityContainer _container;
        private readonly IShellWindowFactory _shellWindowFactory;
        private readonly IViewModelResolver _viewModelResolver;

        public WindowService(IUnityContainer container, IViewModelResolver viewModelResolver,
            IShellWindowFactory shellWindowFactory, IWindow window)
        {
            Window = window;
            _viewModelResolver = viewModelResolver;
            _container = container;
            _shellWindowFactory = shellWindowFactory;
        }

        public IWindow Window { get; }

        private IShellWindow Initialize(Type viewModelType, Action<object> configureViewModel,
            Action<IShellWindow> configureWindow, Action<IUnityContainer> setupContainer, out object viewModel)
        {
            var viewType = _viewModelResolver.ResolveViewType(viewModelType);
            var window = _shellWindowFactory.Create();

            StatusBarManager statusBar = null;

            var windowContainer = _container.CreateChildContainer();

            foreach (var implementedInterface in window.GetType().GetInterfaces())
                windowContainer.RegisterInstance(implementedInterface, window);

            windowContainer.RegisterInstance(window);
            windowContainer.RegisterSingleton<IShellStatusBar>(new InjectionFactory(container =>
                statusBar = new StatusBarManager()));
            windowContainer.RegisterType(viewType);
            windowContainer.RegisterType(viewModelType);

            windowContainer.RegisterSingleton<WindowService>();
            windowContainer.RegisterType<IWindowService, WindowService>();
            windowContainer.RegisterType<IWindowInteractionService, WindowService>();

            setupContainer?.Invoke(windowContainer);

            viewModel = windowContainer.Resolve(viewModelType);
            configureViewModel?.Invoke(viewModel);

            var view = (FrameworkElement)windowContainer.Resolve(viewType);
            view.DataContext = viewModel;

            window.InitalizeContent(view, statusBar);
            SetupWindowClosed(window, windowContainer);

            if (double.IsNaN(window.Height) && double.IsNaN(window.Width))
                window.SizeToContent = SizeToContent.WidthAndHeight;
            else if (double.IsNaN(window.Height))
                window.SizeToContent = SizeToContent.Height;
            else if (double.IsNaN(window.Width))
                window.SizeToContent = SizeToContent.Width;

            if (viewModel is INavigationAware navigationAware)
            {
                void OnViewLoaded(object s, EventArgs e)
                {
                    navigationAware.OnNavigatedTo(null);
                    view.Loaded -= OnViewLoaded;
                }

                void OnWindowClosed(object s, EventArgs e)
                {
                    navigationAware.OnNavigatedFrom(null);
                    window.Closed -= OnWindowClosed;
                }

                view.Loaded += OnViewLoaded;
                window.Closed += OnWindowClosed;
            }

            if (viewModel is IActiveAware activeAware)
            {
                void OnViewLoaded(object s, EventArgs e)
                {
                    activeAware.IsActive = true;
                    view.Loaded -= OnViewLoaded;
                }

                void OnWindowClosed(object s, EventArgs e)
                {
                    activeAware.IsActive = false;
                    window.Closed -= OnWindowClosed;
                }

                view.Loaded += OnViewLoaded;
                window.Closed += OnWindowClosed;
            }

            configureWindow?.Invoke(window);
            return window;
        }

        private static void SetupWindowClosed(IShellWindow window, IUnityContainer lifescope)
        {
            window.Closed += (sender, args) => lifescope.Dispose();
        }

        public bool? ShowDialog(Type viewModelType, Action<IUnityContainer> configureContainer,
            Action<IShellWindow> configureWindow, Action<object> configureViewModel, out object viewModel)
        {
            var window = Initialize(viewModelType, configureViewModel, configureWindow, configureContainer,
                out viewModel);
            return window.ShowDialog(Window);
        }

        public void Show(Type viewModelType, Action<IUnityContainer> configureContainer,
            Action<IShellWindow> configureWindow, Action<object> configureViewModel, out object viewModel)
        {
            var window = Initialize(viewModelType, configureViewModel, configureWindow, configureContainer,
                out viewModel);
            window.Show(Window);
        }

        public bool? ShowDialog(VistaFileDialog fileDialog) => fileDialog.ShowDialog(Window as Window);

        public bool? ShowDialog(FileDialog fileDialog) => fileDialog.ShowDialog(Window as Window);

        public bool? ShowDialog(VistaFolderBrowserDialog folderDialog) => folderDialog.ShowDialog(Window as Window);

        public MessageBoxResult ShowMessageBox(string text, string caption, MessageBoxButton buttons,
            MessageBoxImage icon, MessageBoxResult defResult, MessageBoxOptions options) =>
            MessageBoxEx.Show(Window as Window, text, caption, buttons, icon, defResult, options);
    }
}