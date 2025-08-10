using System.Windows.Input;

namespace TaskPro.Services
{
    public abstract class AsyncCommandBase : ICommand
    {
        private readonly Action<Exception> _onException;
        public event EventHandler? CanExecuteChanged; // Fix for CS8612: Allow nullability for EventHandler

        private bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            private set
            {
                if (_isExecuting != value)
                {
                    _isExecuting = value;
                    OnCanExecuteChanged();
                }
            }
        }

        public AsyncCommandBase(Action<Exception>? onException = null)
        {
            _onException = onException!;
        }

        public bool CanExecute(object? parameter) // Fix for CS8767: Allow nullability for parameter
        {
            return !IsExecuting;
        }

        public async void Execute(object? parameter) // Fix for CS8767: Allow nullability for parameter
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter!); // Ensure parameter is not null when passed to ExecuteAsync
            }
            catch (Exception ex)
            {
                _onException?.Invoke(ex);
            }

            IsExecuting = false;
        }

        protected abstract Task ExecuteAsync(object parameter);

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}

