using Firebase.Auth;
using System.Text.RegularExpressions;
using TaskPro1.ViewModels;

namespace TaskPro1.Services.Commands.Authentication
{
    public partial class LoginCommand : AsyncCommandBase
    {
        private readonly FirebaseAuthClient _authClient;
        private readonly LoginViewModel _loginViewModel;

        public LoginCommand(LoginViewModel loginViewModel, FirebaseAuthClient client)
        {
            _loginViewModel = loginViewModel;
            _authClient = client;
        }
        protected override async Task ExecuteAsync(object parameter)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            if (!Regex.IsMatch(_loginViewModel.Email, regex, RegexOptions.IgnoreCase))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Email Address", "Ok");
                return;
            }
            try
            {
                await _authClient.SignInWithEmailAndPasswordAsync(_loginViewModel.Email, _loginViewModel.Password);
                await Application.Current.MainPage.DisplayAlert("Success", "Successfully Loggged in", "Ok");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert($"{ex}  Error", "Failed to Login, please try again later", "Ok");
            }
        }
    }
}


