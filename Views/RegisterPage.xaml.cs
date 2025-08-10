using TaskPro1.ViewModels;

namespace TaskPro1.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(RegisterViewModel registerViewModel)
        {
            InitializeComponent();

            BindingContext = registerViewModel;
        }

    }
}