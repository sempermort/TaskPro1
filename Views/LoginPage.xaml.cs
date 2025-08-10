using TaskPro1.ViewModels;
namespace TaskPro1.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel loginViewModel)
        {
            InitializeComponent();

            BindingContext = loginViewModel;
        }

    }
}