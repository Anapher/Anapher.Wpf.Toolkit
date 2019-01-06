using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace Anapher.Wpf.Toolkit
{
    /// <summary>
    ///     A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default
    ///     return value for the CanExecute method is 'true' when the command is not executing.
    /// </summary>
    public class AsyncDelegateCommand<T> : DelegateCommandBase
    {
        private readonly Func<T, Task> _executeMethod;
        private Func<T, bool> _canExecuteMethod;
        private bool _isExecuting;

        /// <summary>
        ///     Creates a new instance of <see cref="Anapher.Wpf.Toolkit.AsyncDelegateCommand{T}" />
        /// </summary>
        /// <param name="executeMethod">
        ///     The <see cref="Func{Task}" /> to invoke when <see cref="ICommand.Execute(object)" /> is
        ///     called.
        /// </param>
        public AsyncDelegateCommand(Func<T, Task> executeMethod) : this(executeMethod, o => true)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="Anapher.Wpf.Toolkit.AsyncDelegateCommand{T}" />
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action" /> to invoke when <see cref="ICommand.Execute" /> is called.</param>
        /// <param name="canExecuteMethod">
        ///     The <see cref="Func{TResult}" /> to invoke when <see cref="ICommand.CanExecute" /> is
        ///     called
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     When both <paramref name="executeMethod" /> and
        ///     <paramref name="canExecuteMethod" /> are <see langword="null" />.
        /// </exception>
        public AsyncDelegateCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "Neither the executeMethod nor the canExecuteMethod delegates can be null.");

            var genericTypeInfo = typeof(T);

            // DelegateCommand allows object or Nullable<>.  
            // note: Nullable<> is a struct so we cannot use a class constraint.
            if (genericTypeInfo.IsValueType)
                if (!genericTypeInfo.IsGenericType || !typeof(Nullable<>).IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition()))
                    throw new InvalidCastException("T for DelegateCommand&lt;T&gt; is not an object nor Nullable.");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        ///     Executes the command and invokes the <see cref="Func{T, Task}" /> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public async Task Execute(T parameter)
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();

            await _executeMethod(parameter);

            _isExecuting = false;
            RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Determines if the command can execute by invoked the <see cref="Func{T,Bool}" /> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command to determine if it can execute.</param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute(T parameter) => _canExecuteMethod(parameter);

        protected override async void Execute(object parameter)
        {
            await Execute((T) parameter);
        }

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.CanExecute(object)" />
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns><see langword="true" /> if the Command Can Execute, otherwise <see langword="false" /></returns>
        protected override bool CanExecute(object parameter) => !_isExecuting && CanExecute((T) parameter);

        /// <summary>
        ///     Observes a property that implements INotifyPropertyChanged, and automatically calls
        ///     DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="TType">The type of the return value of the method that this delegate encapulates</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public AsyncDelegateCommand<T> ObservesProperty<TType>(Expression<Func<TType>> propertyExpression)
        {
            ObservesPropertyInternal(propertyExpression);
            return this;
        }

        /// <summary>
        ///     Observes a property that is used to determine if this command can execute, and if it implements
        ///     INotifyPropertyChanged it will automatically call DelegateCommandBase.RaiseCanExecuteChanged on property changed
        ///     notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public AsyncDelegateCommand<T> ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            var expression = Expression.Lambda<Func<T, bool>>(canExecuteExpression.Body, Expression.Parameter(typeof(T), "o"));
            _canExecuteMethod = expression.Compile();
            ObservesPropertyInternal(canExecuteExpression);
            return this;
        }
    }
}