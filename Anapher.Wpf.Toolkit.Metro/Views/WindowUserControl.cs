using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Anapher.Wpf.Toolkit.Metro.Windows;
using MahApps.Metro.Controls;
using Unity.Attributes;

namespace Anapher.Wpf.Toolkit.Metro.Views
{
    public class WindowUserControl : UserControl
    {
        public static readonly DependencyProperty EscapeClosesWindowProperty = DependencyProperty.Register(
            "EscapeClosesWindow", typeof(bool), typeof(WindowUserControl), new PropertyMetadata(default(bool),
                (o, args) =>
                {
                    if (GetViewManager(o, out var viewManager)) viewManager.EscapeClosesWindow = (bool) args.NewValue;
                }));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(WindowUserControl),
            new PropertyMetadata(default(string), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.Title = (string) args.NewValue;
            }));

        public static readonly DependencyProperty RightStatusBarContentProperty =
            DependencyProperty.Register("RightStatusBarContent", typeof(object), typeof(WindowUserControl),
                new PropertyMetadata(default, (o, args) =>
                {
                    if (GetViewManager(o, out var viewManager))
                        viewManager.RightStatusBarContent = args.NewValue;
                }));

        public static readonly DependencyProperty LeftWindowCommandsProperty = DependencyProperty.Register(
            "LeftWindowCommands", typeof(WindowCommands), typeof(WindowUserControl),
            new PropertyMetadata(default(WindowCommands), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.LeftWindowCommands = (WindowCommands) args.NewValue;
            }));

        public static readonly DependencyProperty RightWindowCommandsProperty = DependencyProperty.Register(
            "RightWindowCommands", typeof(WindowCommands), typeof(WindowUserControl),
            new PropertyMetadata(default(WindowCommands), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.RightWindowCommands = (WindowCommands) args.NewValue;
            }));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object),
            typeof(WindowUserControl),
            new PropertyMetadata(default, (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.TitleBarIcon = args.NewValue;
            }));

        public static readonly DependencyProperty FlyoutsProperty = DependencyProperty.Register("Flyouts",
            typeof(FlyoutsControl), typeof(WindowUserControl),
            new PropertyMetadata(default(FlyoutsControl), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.Flyouts = (FlyoutsControl) args.NewValue;
            }));

        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.Register("DialogResult",
            typeof(bool?), typeof(WindowUserControl),
            new PropertyMetadata(default(bool?), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.DialogResult = (bool?) args.NewValue;
            }));

        public static readonly DependencyProperty ShowInTaskBarProperty = DependencyProperty.Register("ShowInTaskBar",
            typeof(bool), typeof(WindowUserControl),
            new PropertyMetadata(default(bool), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.ShowInTaskbar = (bool) args.NewValue;
            }));

        public static readonly DependencyProperty TaskBarIconProperty = DependencyProperty.Register("TaskBarIcon",
            typeof(ImageSource), typeof(WindowUserControl),
            new PropertyMetadata(default(ImageSource), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.TaskBarIcon = (ImageSource) args.NewValue;
            }));

        public static readonly DependencyProperty WindowWidthProperty = DependencyProperty.Register("WindowWidth",
            typeof(double), typeof(WindowUserControl),
            new PropertyMetadata(default(double), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.Width = (double) args.NewValue;
            }));

        public static readonly DependencyProperty WindowHeightProperty = DependencyProperty.Register("WindowHeight",
            typeof(double), typeof(WindowUserControl),
            new PropertyMetadata(default(double), (o, args) =>
            {
                if (GetViewManager(o, out var viewManager))
                    viewManager.Height = (double) args.NewValue;
            }));

        private ResizeMode? _resizeMode;

        public IMetroShellWindow ViewManager { get; set; }

        [InjectionMethod]
        public void InitializeViewManager(IMetroShellWindow viewManager)
        {
            var height = WindowHeight;
            if (height != default)
                viewManager.Height = height;

            var width = WindowWidth;
            if (width != default)
                viewManager.Width = width;

            if (_resizeMode.HasValue)
                viewManager.ResizeMode = _resizeMode.Value;

            var taskBarIcon = TaskBarIcon;
            if (taskBarIcon != default)
                viewManager.TaskBarIcon = taskBarIcon;

            var showInTaskbar = ShowInTaskBar;
            if (showInTaskbar != default)
                viewManager.ShowInTaskbar = showInTaskbar;

            var dialogResult = DialogResult;
            if (dialogResult != default)
                viewManager.DialogResult = dialogResult;

            var title = Title;
            if (title != default)
                viewManager.Title = title;

            var rightStatusBarContent = RightStatusBarContent;
            if (rightStatusBarContent != default)
                viewManager.RightStatusBarContent = rightStatusBarContent;

            var leftWindowCommands = LeftWindowCommands;
            if (leftWindowCommands != default)
                viewManager.LeftWindowCommands = leftWindowCommands;

            var rightWindowCommands = RightWindowCommands;
            if (rightWindowCommands != default)
                viewManager.RightWindowCommands = rightWindowCommands;

            var icon = Icon;
            if (icon != default)
                viewManager.TitleBarIcon = icon;

            var flyouts = Flyouts;
            if (flyouts != default)
                viewManager.Flyouts = flyouts;

            var escapeClosesWindow = EscapeClosesWindow;
            if (escapeClosesWindow != default)
                viewManager.EscapeClosesWindow = escapeClosesWindow;

            ViewManager = viewManager;
        }

        public double WindowHeight
        {
            get => (double)GetValue(WindowHeightProperty);
            set => SetValue(WindowHeightProperty, value);
        }

        public double WindowWidth
        {
            get => (double)GetValue(WindowWidthProperty);
            set => SetValue(WindowWidthProperty, value);
        }

        public ResizeMode ResizeMode
        {
            get => ViewManager?.ResizeMode ?? _resizeMode ?? ResizeMode.CanResize;
            set
            {
                if (ViewManager == null)
                    _resizeMode = value;
                else
                    ViewManager.ResizeMode = value;
            }
        }

        public ImageSource TaskBarIcon
        {
            get => (ImageSource)GetValue(TaskBarIconProperty);
            set => SetValue(TaskBarIconProperty, value);
        }

        public bool ShowInTaskBar
        {
            get => (bool)GetValue(ShowInTaskBarProperty);
            set => SetValue(ShowInTaskBarProperty, value);
        }

        public bool? DialogResult
        {
            get => (bool?)GetValue(DialogResultProperty);
            set => SetValue(DialogResultProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public object RightStatusBarContent
        {
            get => GetValue(RightStatusBarContentProperty);
            set => SetValue(RightStatusBarContentProperty, value);
        }

        public WindowCommands LeftWindowCommands
        {
            get => (WindowCommands)GetValue(LeftWindowCommandsProperty);
            set => SetValue(LeftWindowCommandsProperty, value);
        }

        public WindowCommands RightWindowCommands
        {
            get => (WindowCommands)GetValue(RightWindowCommandsProperty);
            set => SetValue(RightWindowCommandsProperty, value);
        }

        public object Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public FlyoutsControl Flyouts
        {
            get => (FlyoutsControl)GetValue(FlyoutsProperty);
            set => SetValue(FlyoutsProperty, value);
        }

        public bool EscapeClosesWindow
        {
            get => (bool)GetValue(EscapeClosesWindowProperty);
            set => SetValue(EscapeClosesWindowProperty, value);
        }

        private static bool GetViewManager(DependencyObject d, out IMetroShellWindow shellWindow)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
            {
                shellWindow = null;
                return false;
            }

            var userControl = (WindowUserControl) d;
            shellWindow = userControl.ViewManager;
            return shellWindow != null;
        }
    }
}
