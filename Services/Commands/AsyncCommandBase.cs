using System.Windows.Input;

namespace TaskPro1.Services.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        private readonly Action<Exception> _onException;
        public event EventHandler? CanExecuteChanged;

        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            private set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }


        public AsyncCommandBase(Action<Exception>? onException = null)
        {
            _onException = onException!;
        }

        public bool CanExecute(object? parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter!);
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

