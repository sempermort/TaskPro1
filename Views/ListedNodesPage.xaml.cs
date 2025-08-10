// filepath: /c:/Users/GOAT/source/repos/TaskPro1/Views/ListedNodesPage.xaml.cs
using Microsoft.Maui.Controls;
using TaskPro1.ViewModels;

namespace TaskPro1.Views
{
    public partial class ListedNodesPage : ContentView
    {
        public ListedNodesPage()
        {
            InitializeComponent();
            var viewModel = new ListedNodesViewModel();
            BindingContext = viewModel;
        }
    }
}