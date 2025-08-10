using TaskPro1.Models;
using TaskPro1.ViewModels;

namespace TaskPro1.Views;

public partial class SearchView : ContentView
{
    private SearchViewModel _viewModel;
    public event EventHandler<Node>? ZoomRequestedFromView;
     
    public SearchView()
    {
        InitializeComponent();
        this.BindingContextChanged += SearchView_BindingContextChanged;
    }
    public SearchView(SearchViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel=vm;

        _viewModel.ZoomRequested += OnZoomRequested;
    
        this.BindingContextChanged += SearchView_BindingContextChanged;
    }

    private void SearchView_BindingContextChanged(object? sender, EventArgs e)
    {
        if (BindingContext is SearchViewModel vm)
        {
            vm.ZoomRequested -= OnZoomRequested; // avoid duplicates
            vm.ZoomRequested += OnZoomRequested;
        }
    }
    private void OnZoomRequested(object? sender, Node e)
    {
        ZoomRequestedFromView?.Invoke(this, e);
    }
}
