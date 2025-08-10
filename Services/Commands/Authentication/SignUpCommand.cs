using Firebase.Auth;
using System.Text.RegularExpressions;
using TaskPro1.ViewModels;
using Application = Microsoft.Maui.Controls.Application;

namespace TaskPro1.Services.Commands.Authentication
{
    public class SignUpCommand : AsyncCommandBase
    {
        private readonly FirebaseAuthClient _authClient;
        private readonly RegisterViewModel _registerViewModel;

        public SignUpCommand(RegisterViewModel registerViewModel, FirebaseAuthClient client)
        {
            _registerViewModel = registerViewModel;
            _authClient = client;
        }
        protected override async Task ExecuteAsync(object parameter)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            bool result = _registerViewModel.Password.Count() >= 6;
          

            if (_registerViewModel.Password != _registerViewModel.ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Password and confirmed password do not match", "Ok");
                return;
            }
            else if (!result)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Email Password", "Ok");
                return;
            }
            else if (!Regex.IsMatch(_registerViewModel.Email, regex, RegexOptions.IgnoreCase))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Email Address", "Ok");
                return;
            }
            try
            {
                await _authClient.CreateUserWithEmailAndPasswordAsync(_registerViewModel.Email, _registerViewModel.Password);
                await Application.Current.MainPage.DisplayAlert("Success", "Successfully signed up", "Ok");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert($"{ex}  >Error", "Failed to sign up, please try again later", "Ok");
            }
        }
    }
}
