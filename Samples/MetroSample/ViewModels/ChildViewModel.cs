using System;
using System.Threading.Tasks;
using System.Windows;
using Anapher.Wpf.Toolkit;
using Anapher.Wpf.Toolkit.Extensions;
using Anapher.Wpf.Toolkit.StatusBar;
using Anapher.Wpf.Toolkit.Windows;
using Prism;
using Prism.Commands;
using Prism.Mvvm;

namespace MetroSample.ViewModels
{
    public class ChildViewModel : BindableBase, IActiveAware
    {
        private readonly IShellStatusBar _statusBar;
        private readonly IWindowService _windowService;
        private AsyncDelegateCommand _buildCommand;
        private bool _isActive;
        private DelegateCommand _showMessageCommand;

        public ChildViewModel(IWindowService windowService, IShellStatusBar statusBar)
        {
            _windowService = windowService;
            _statusBar = statusBar;
        }

        public DelegateCommand ShowMessageCommand
        {
            get
            {
                return _showMessageCommand ?? (_showMessageCommand = new DelegateCommand(() =>
                {
                    _windowService.ShowMessage("This message box will be on top of the child window", "Information", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    _statusBar.ShowMessage("Message Box opened successfully!");
                }));
            }
        }

        public AsyncDelegateCommand BuildCommand
        {
            get
            {
                return _buildCommand ?? (_buildCommand = new AsyncDelegateCommand(async () =>
                {
                    await _statusBar.ShowMessage("Building...", Task.Delay(5000), StatusBarAnimation.Build);
                }));
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (value)
                    _statusBar.ShowSuccess("Window opened successfully!");
            }
        }

        public event EventHandler IsActiveChanged;
    }
}