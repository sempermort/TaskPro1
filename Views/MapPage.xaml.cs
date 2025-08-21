using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using SkiaSharp;
using TaskPro1.Helpers;
using TaskPro1.Helpers.Interfaces;
using TaskPro1.ViewModels;
using Windows.Devices.Geolocation;

namespace TaskPro1.Views;

public partial class MapPage : ContentPage
{
    private double zoomLevel = 16;
 
    private Layer? _markersLayer;
    private MapPageCompositeViewModel? viewModel;
    private IMapOperationsService? _mapOperationsService;
    public string Icon { get; set; } = "/uf041"; // bindable or injected

    private MapPage()
    {
        InitializeComponent();
        SetDefaultBindingContext();
    }

    public MapPage(MapPageCompositeViewModel compositeVM)
    {
        InitializeComponent();
        viewModel = compositeVM;
        SetCompositeBindingContext(compositeVM.MapPageVM, compositeVM.SearchVM);        

    }

    private void SetDefaultBindingContext()
    {
        BindingContext = this;
    }

    private void SetCompositeBindingContext(MapPageViewModel mapPageVM, SearchViewModel searchVM)
    {
        BindingContext = new MapPageCompositeViewModel(mapPageVM, searchVM);
    }

    private void Features_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        mapView.Refresh(); // Refresh the map view when features change
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CheckLocationPermissionRequestAsync();        
        await LoadMapAsync();
    }

    private async Task LoadMapAsync()
    {
        try
        {
            var location = await GetCurrentLocationAsync();
            if (location == null) return;

            if (viewModel == null) return;

            if (mapView.Map == null)
                mapView.Map = new Mapsui.Map();

            await viewModel.MapPageVM.InitializeAsync(mapView.Map, TestData.Nodes, Icon);

            // Use the map operations service to center the map
            _mapOperationsService?.CenterMapOnLocation(location.Longitude, location.Latitude, zoomLevel);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Location error: {ex.Message}", "OK");
        }
    }

    private async Task<Location?> GetCurrentLocationAsync()
    {
        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(30));
        return await Geolocation.GetLocationAsync(request);
    }

    private async Task<int> GetPinBitmapId(string iconCode)
    {
        string fontPath = "FluentSystemIconsRegular.ttf";
        return await FontAwesomeHelper.CreateFontAwesomePin(iconCode, fontPath, SKColors.Red, 64);
    }

    private async Task CheckLocationPermissionRequestAsync()
    {
#if ANDROID || IOS
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
                await DisplayAlert("Location Permission", "Location access is required for map functionality.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Location permission error: {ex.Message}", "OK");
        }
#elif WINDOWS
        var accessStatus = await Geolocator.RequestAccessAsync();
        if (accessStatus != GeolocationAccessStatus.Allowed)
            await DisplayAlert("Location Access", "Enable location in system settings.", "OK");
#endif
    } 

    private void OnMenuTapped(object sender, EventArgs e) =>
        DisplayAlert("Tapped", "Image was tapped!", "OK");

    private void OnButtonClicked(object sender, EventArgs e) =>
        DisplayAlert("Clicked", "Button was Clicked!", "OK");

    private void OnDoneClicked(object sender, EventArgs e) =>
        DisplayAlert("Done", "It's done!", "OK");

    private void OnDragHandlePanUpdated(object sender, PanUpdatedEventArgs e) =>
        Console.WriteLine($"Pan: {e.StatusType}, X={e.TotalX}, Y={e.TotalY}");
        
    private void OnSearchViewTapped(object sender, EventArgs e) =>
        SetGridProportions(4, 6);

    private void OnMapTapped(object sender, EventArgs e) =>
        SetGridProportions(7, 3);

    private void SetGridProportions(double searchRowStar, double mapRowStar)
    {
        MainGrid.RowDefinitions[0].Height = new GridLength(searchRowStar, GridUnitType.Star);
        MainGrid.RowDefinitions[2].Height = new GridLength(mapRowStar, GridUnitType.Star);
    }
}
