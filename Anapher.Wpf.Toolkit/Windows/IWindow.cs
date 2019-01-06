using System;
using System.ComponentModel;
using System.Windows;

namespace Anapher.Wpf.Toolkit.Windows
{
    /// <summary>
    ///     Represents a simple window
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        ///     Get/set the current window state
        /// </summary>
        WindowState WindowState { get; set; }

        /// <summary>
        ///     The closed event of the window
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        ///     The closing event of the window
        /// </summary>
        event CancelEventHandler Closing;

        /// <summary>
        ///     Close the window
        /// </summary>
        void Close();

        /// <summary>
        ///     Activate the window
        /// </summary>
        bool Activate();
    }
}