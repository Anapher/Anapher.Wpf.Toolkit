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
    public class AsyncDelegateCommand : DelegateCommandBase
    {
        private Func<bool> _canExecuteMethod;
        private readonly Func<Task> _executeMethod;
        private bool _isExecuting;

        /// <summary>
        ///     Creates a new instance of <see cref="AsyncDelegateCommand" /> with the <see cref="Func{Task}" /> to invoke on
        ///     execution.
        /// </summary>
        /// <param name="executeMethod">
        ///     The <see cref="Func{Task}" /> to invoke when <see cref="ICommand.Execute(object)" /> is
        ///     called.
        /// </param>
        public AsyncDelegateCommand(Func<Task> executeMethod) : this(executeMethod, () => true)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="AsyncDelegateCommand" /> with the <see cref="Func{Task}" /> to invoke on
        ///     execution
        ///     and a <see langword="Func" /> to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action" /> to invoke when <see cref="ICommand.Execute" /> is called.</param>
        /// <param name="canExecuteMethod">
        ///     The <see cref="Func{TResult}" /> to invoke when <see cref="ICommand.CanExecute" /> is
        ///     called
        /// </param>
        public AsyncDelegateCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "Neither the executeMethod nor the canExecuteMethod delegates can be null.");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        ///<summary>
        /// Executes the command.
        ///</summary>
        public async Task Execute()
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();

            await _executeMethod();

            _isExecuting = false;
            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the command can execute,otherwise returns <see langword="false"/>.</returns>
        public bool CanExecute()
        {
            return !_isExecuting && _canExecuteMethod();
        }

        protected override async void Execute(object parameter)
        {
            await Execute();
        }

        protected override bool CanExecute(object parameter) => CanExecute();

        /// <summary>
        /// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public AsyncDelegateCommand ObservesProperty<T>(Expression<Func<T>> propertyExpression)
        {
            ObservesPropertyInternal(propertyExpression);
            return this;
        }

        /// <summary>
        /// Observes a property that is used to determine if this command can execute, and if it implements INotifyPropertyChanged it will automatically call DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public AsyncDelegateCommand ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            _canExecuteMethod = canExecuteExpression.Compile();
            ObservesPropertyInternal(canExecuteExpression);
            return this;
        }
    }
}