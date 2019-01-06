using System.Windows;
using Anapher.Wpf.Toolkit.Windows;

namespace Anapher.Wpf.Toolkit.Extensions
{
    public static class WindowInteractionExtensions
    {
        /// <summary>
        ///     Displays a message box.
        /// </summary>
        /// <param name="owner">The window which will own the message box</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the <see cref="MessageBoxResult" /> values</returns>
        public static MessageBoxResult ShowMessage(this IWindowInteractionService owner, string text) =>
            owner.ShowMessageBox(text, null, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None,
                MessageBoxOptions.None);

        /// <summary>
        ///     Displays a message box.
        /// </summary>
        /// <param name="owner">The window which will own the message box</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <returns>One of the <see cref="MessageBoxResult" /> values</returns>
        public static MessageBoxResult ShowMessage(this IWindowInteractionService owner, string text, string caption) =>
            owner.ShowMessageBox(text, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None,
                MessageBoxOptions.None);

        /// <summary>
        ///     Displays a message box.
        /// </summary>
        /// <param name="owner">The window which will own the message box</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">
        ///     One of the <see cref="MessageBoxButton" /> values that specifies which buttons to display in the
        ///     message box.
        /// </param>
        /// <returns>One of the <see cref="MessageBoxResult" /> values</returns>
        public static MessageBoxResult ShowMessage(this IWindowInteractionService owner, string text, string caption,
            MessageBoxButton buttons) =>
            owner.ShowMessageBox(text, caption, buttons, MessageBoxImage.None, MessageBoxResult.None,
                MessageBoxOptions.None);

        /// <summary>
        ///     Displays a message box.
        /// </summary>
        /// <param name="owner">The window which will own the message box</param>
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
        /// <returns>One of the <see cref="MessageBoxResult" /> values</returns>
        public static MessageBoxResult ShowMessage(this IWindowInteractionService owner, string text, string caption,
            MessageBoxButton buttons, MessageBoxImage icon) =>
            owner.ShowMessageBox(text, caption, buttons, icon, MessageBoxResult.None, MessageBoxOptions.None);

        /// <summary>
        ///     Displays a message box.
        /// </summary>
        /// <param name="owner">The window which will own the message box</param>
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
        /// <returns>One of the <see cref="MessageBoxResult" /> values</returns>
        public static MessageBoxResult ShowMessage(this IWindowInteractionService owner, string text, string caption,
            MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult) =>
            owner.ShowMessageBox(text, caption, buttons, icon, defResult, MessageBoxOptions.None);
    }
}