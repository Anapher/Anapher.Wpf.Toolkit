using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Anapher.Wpf.Toolkit.Extensions;
using Anapher.Wpf.Toolkit.Metro.Windows;
using Anapher.Wpf.Toolkit.StatusBar;
using Anapher.Wpf.Toolkit.Windows;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

namespace Anapher.Wpf.Toolkit.Metro.Views
{
    public class MetroShellWindow : MetroWindow, IMetroShellWindow
    {
        private object _rightStatusBarContent;
        private StatusBarManager _statusBarManager;
        private object _titleBarIcon;

        public MetroShellWindow()
        {
            Title = "Default Shell Window";
            Style = (Style) FindResource("ChildWindow");
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowIconOnTitleBar = false;
        }

        public object TitleBarIcon
        {
            get => _titleBarIcon;
            set
            {
                if (_titleBarIcon != value && value != null)
                {
                    if (Icon == null && value is ImageSource imageSource)
                    {
                        Icon = imageSource;
                    }
                    else
                    {
                        var factory = new FrameworkElementFactory(typeof(ContentPresenter));
                        factory.SetValue(ContentPresenter.ContentProperty, new Binding {Source = value});

                        IconTemplate = new DataTemplate {VisualTree = factory};
                        _titleBarIcon = value;
                    }

                    ShowIconOnTitleBar = true;
                }
            }
        }

        public ImageSource TaskBarIcon
        {
            get => Icon;
            set
            {
                Icon = value;
                ShowIconOnTitleBar = true;
            }
        }

        public object RightStatusBarContent
        {
            get => _statusBarManager?.RightContent;
            set
            {
                if (_statusBarManager != null)
                    _statusBarManager.RightContent = value;
                else _rightStatusBarContent = value;
            }
        }

        public bool EscapeClosesWindow
        {
            get => WindowExtensions.GetEscapeClosesWindow(this);
            set => WindowExtensions.SetEscapeClosesWindow(this, value);
        }

        public void InitalizeContent(object content, StatusBarManager statusBarManager)
        {
            if (statusBarManager == null)
            {
                Content = content;
            }
            else
            {
                _statusBarManager = statusBarManager;
                statusBarManager.RightContent = _rightStatusBarContent;

                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(1, GridUnitType.Star)});
                grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});

                var statusBar = new Controls.StatusBar {ShellStatusBar = statusBarManager};
                Grid.SetRow(statusBar, 1);
                grid.Children.Add(statusBar);

                if (content is UIElement uiElement)
                {
                    Grid.SetRow(uiElement, 0);
                    grid.Children.Add(uiElement);
                }
                else
                {
                    var contentControl = new ContentControl {Content = content};
                    Grid.SetRow(contentControl, 0);
                    grid.Children.Add(contentControl);
                }

                Content = grid;
            }

            if (content is FrameworkElement fw) DataContext = fw.DataContext;
        }

        public void Show(IWindow owner)
        {
            Owner = owner as Window;
            Show();
        }

        public bool? ShowDialog(IWindow owner)
        {
            Owner = owner as Window;
            return ShowDialog();
        }

        public bool? ShowDialog(VistaFileDialog fileDialog) => fileDialog.ShowDialog(this);

        public bool? ShowDialog(FileDialog fileDialog) => fileDialog.ShowDialog(this);

        public bool? ShowDialog(VistaFolderBrowserDialog folderDialog) => folderDialog.ShowDialog(this);
    }
}