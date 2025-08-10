
using System.Runtime.CompilerServices;

namespace TaskPro1.Controls
{
    public partial class SearchHeaderView : Grid
    {
        public Thickness BackButtonPadding { get; set; } = new Thickness(0, 0, 0, 0);
        public SearchHeaderView() => InitializeComponent();

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == IsVisibleProperty.PropertyName)
            {
                if (IsVisible)
                {
                    FocusDestination();
                    ic_Back.Margin = BackButtonPadding;
                }
            }
        }

        public void FocusDestination() => destinationEntry.Focus();
    }
}