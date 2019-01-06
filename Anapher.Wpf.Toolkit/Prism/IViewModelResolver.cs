using System;

namespace Anapher.Wpf.Toolkit.Prism
{
    public interface IViewModelResolver
    {
        Type ResolveViewModelType(Type viewType);
        Type ResolveViewType(Type viewModelType);
    }
}