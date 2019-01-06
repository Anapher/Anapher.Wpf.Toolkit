using Anapher.Wpf.Toolkit.Extensions;
using Anapher.Wpf.Toolkit.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace MetroSample.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IWindowService _windowService;
        private DelegateCommand _openWindowCommand;

        public MainViewModel(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public DelegateCommand OpenWindowCommand
        {
            get
            {
                return _openWindowCommand ?? (_openWindowCommand = new DelegateCommand(() => { _windowService.ShowDialog<ChildViewModel>(); }));
            }
        }
    }
}