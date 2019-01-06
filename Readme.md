# Anapher.Wpf.Toolkit
A class library for WPF that provides useful classes for MVVM and the UI.

## Packages
This library depends on the following packages:
- [Prism](https://github.com/PrismLibrary/Prism) - the MVVM framework
- [Ookii.Dialogs.Wpf](https://github.com/caioproiete/ookii-dialogs-wpf) - Common dialog classes for WPF applications

# Core features
### Status Bar
Using the toolkit and the Prism framework, implementing a status bar is very, very easy. You just have to request `IShellStatusBar` in the view model constructor and the status bar will automatically be added to the view. The status bar provides useful extensions to display notifications aswell as processes.

### Message Box
With the toolkit, you can easily open message boxes in your view that are centered on the window. Just request the `IWindowService` and you can open up a message box using `IWindowService.ShowMessage(...)`

### Prism
Prism is a very nice framework for complex applications, but the window handling of Prism (using IInteractionRequest) is over-complicated and too verbose. I implemented a different way to to open windows using the `IWindowService`. Just request this interface in the constructor of your window and you can open other windows using `IWindowService.Show<TViewModel>()` or `IWindowService.ShowDialog<TViewModel>`. The view will be resolved by convention in the `IViewModelResolver`. `Show<>` and `ShowDialog<>` have lots of overloads, you can call methods of the view model before the window is opened aswell as getting the view model using an out parameter.

### AsyncDelegateCommand
`AsyncDelegateCommand` and `AsyncDelegateCommand{T}` were added that take a task as delegate. While the task is executing, the `ICommand.CanExecute` will switch to `false`.

### TransactionalObservableCollection
The `ObservableCollection<T>` is one of the most used classes in WPF, but it doesn't provide methods to make larger changes without emitting thousands of `CollectionChanged` events. The `TransactionalObservableCollection<T>` fixes this issue by providing `AddRange` and `RemoveRange` methods.

### Other extensions and converters
Extensions:
- ControlExtensions: Provides attached properties to set a double click command
- HyperlinkExtensions: Open the hyper link in browser
- PasswordBoxExtensions: Binding for the SecureString and IsEmpty attached property
- WindowExtensions: attached properties ShowIcon, EscapeClosesWindow, DialogResult

Converter:
- CloneConverter: Clone the value. Useful when you want to provide multiple Parameters using a `MultiBinding`
- EnumBooleanConverter: Makes it easy for RadioButtons to set an enum value
- FormatBytesConverter: Format a long to a readable string (KiB, MiB, GiB, ...)
- IconSizeConverter: Get a specifc size from an icon
- IsStringEmptyConverter: Get a boolean that tells whether the string is empty
- RemoveLineBreaksConverter: Remove all line breaks from a string
- SwitchBooleanConverter: Switch a boolean value


# Anapher.Wpf.Toolkit.Metro
Integrate MahApps.Metro and Prism, and get a Windows 10 styled MahApps Window:
![Imgur](https://i.imgur.com/FwQm1uo.png)