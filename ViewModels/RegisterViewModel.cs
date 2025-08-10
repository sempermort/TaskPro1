using Firebase.Auth;
using System.Windows.Input;
using TaskPro1.Services.Commands.Authentication;

namespace TaskPro1.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {


        public ICommand SignUpCommand { get; }

        public RegisterViewModel(FirebaseAuthClient authClient)
        {
            SignUpCommand = new SignUpCommand(this, authClient);
        }
        private string? _email;
        public string? Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string? _password;
        public string? Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string? _confirmPassword;
        public string? ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

    }
}

