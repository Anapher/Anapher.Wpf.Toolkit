using Anapher.Wpf.Toolkit.Windows;

namespace Anapher.Wpf.Toolkit.Prism
{
    /// <summary>
    ///     A factory that created <see cref="IShellWindow" />s.
    /// </summary>
    public interface IShellWindowFactory
    {
        /// <summary>
        ///     Create a new <see cref="IShellWindow" />
        /// </summary>
        /// <returns>Return the created window.</returns>
        IShellWindow Create();
    }
}