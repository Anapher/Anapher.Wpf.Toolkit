using System.Windows;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

namespace Anapher.Wpf.Toolkit.Windows
{
    /// <summary>
    ///     Provide functions to interact with the user on top of the current window.
    /// </summary>
    public interface IWindowInteractionService
    {
        /// <summary>
        ///     Displays a message box.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">
        ///     One of the <see cref="MessageBoxButton" /> values that specifies which buttons to display in the
        ///     message box.
        /// </param>
        /// <param name="icon">
        ///     One of the <see cref="MessageBoxImage" /> values that specifies which icon to display in the message
        ///     box.
        /// </param>
        /// <param name="defResult">
        ///     One of the <see cref="MessageBoxResult" /> values that specifies the default button for the
        ///     message box.
        /// </param>
        /// <param name="options">
        ///     One of the <see cref="MessageBoxOptions" /> values that specifies which display and association
        ///     options will be used for the message box. You may pass in 0 if you wish to use the defaults.
        /// </param>
        /// <returns>One of the <see cref="MessageBoxResult" /> values</returns>
        MessageBoxResult ShowMessageBox(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon,
            MessageBoxResult defResult, MessageBoxOptions options);

        /// <summary>
        ///     Show a <see cref="VistaFileDialog" /> on top of the window
        /// </summary>
        /// <param name="fileDialog">The dialog to show</param>
        /// <returns>Return the dialog result</returns>
        bool? ShowDialog(VistaFileDialog fileDialog);

        /// <summary>
        ///     Show a <see cref="FileDialog" /> on top of the window
        /// </summary>
        /// <param name="fileDialog">The dialog to show</param>
        /// <returns>Return the dialog result</returns>
        bool? ShowDialog(FileDialog fileDialog);

        /// <summary>
        ///     Show a <see cref="VistaFolderBrowserDialog" /> on top of the window
        /// </summary>
        /// <param name="folderDialog">The dialog to show</param>
        /// <returns>Return the dialog result</returns>
        bool? ShowDialog(VistaFolderBrowserDialog folderDialog);
    }
}