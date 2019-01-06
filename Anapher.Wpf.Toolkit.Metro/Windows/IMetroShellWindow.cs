using Anapher.Wpf.Toolkit.Windows;
using MahApps.Metro.Controls;

namespace Anapher.Wpf.Toolkit.Metro.Windows
{
    public interface IMetroShellWindow : IShellWindow
    {
        /// <summary>
        ///     Gets/sets the FlyoutsControl that hosts the window's flyouts.
        /// </summary>
        FlyoutsControl Flyouts { get; set; }

        /// <summary>
        ///     Gets/sets the left window commands that hosts the user commands.
        /// </summary>
        WindowCommands LeftWindowCommands { get; set; }

        /// <summary>
        ///     Gets/sets the right window commands that hosts the user commands.
        /// </summary>
        WindowCommands RightWindowCommands { get; set; }
    }
}