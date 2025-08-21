using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Tiling;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskPro1.Helpers;
using TaskPro1.Helpers.Interfaces;
using TaskPro1.Models;
using TaskPro1.Services;

namespace TaskPro1.ViewModels
{
    public partial class MapPageViewModel : ObservableObject
    {
        private readonly IGoogleMapsApiService _googleMapsApi = new GoogleMapsApiService();
        private readonly FontAwesomeHelper _fontAwesomeHelper;

        public ObservableCollection<PriceOption> PriceOptions { get; private set; }
        public ObservableCollection<PlaceAutoCompletePrediction> Places { get; private set; }

        [ObservableProperty]
        private ObservableCollection<Node> filteredNodes = new(TestData.Nodes);

        [ObservableProperty]
        private PlaceAutoCompletePrediction? recentPlace1;

        [ObservableProperty]
        private PlaceAutoCompletePrediction? recentPlace2;

        [ObservableProperty]
        private PriceOption? priceOptionSelected;

        [ObservableProperty]
        private string? pickupLocation;

        [ObservableProperty]
        private string? destinationLocation;

        [ObservableProperty]
        private PlaceAutoCompletePrediction? placeSelected;

        [ObservableProperty]
        private bool isOriginFocused;

        [ObservableProperty]
        private bool isDestinationFocused;

        [ObservableProperty]
        private int zoomLevel = 16;
        private Mapsui.Map _map;
        public ICommand CenterOnCommand { get; }
        public ICommand ZoomInCommand => new AsyncRelayCommand(OnZoomInAsync);
        public ICommand ZoomOutCommand => new AsyncRelayCommand(OnZoomOutAsync);
        public ICommand DoneCommand => new AsyncRelayCommand(OnDoneClickedAsync);

        public IAsyncRelayCommand<PlaceAutoCompletePrediction> GetPlaceDetailCommand { get; }
        public IAsyncRelayCommand<string> GetPlacesCommand { get; }

        public ObservableCollection<IFeature> Features { get; } = new();

        public MapPageViewModel(FontAwesomeHelper fontAwesomeHelper)
        {
            _fontAwesomeHelper = fontAwesomeHelper;
            

            RecentPlace1 = RecentPlacesStore.RecentPlaces.FirstOrDefault();
            RecentPlace2 = RecentPlacesStore.RecentPlaces.LastOrDefault();
            PriceOptions = new ObservableCollection<PriceOption>(PriceOptionsStore.PriceOptions);
            Places = new ObservableCollection<PlaceAutoCompletePrediction>(RecentPlacesStore.RecentPlaces);
            PriceOptionSelected = PriceOptions.First();

            GetPlacesCommand = new AsyncRelayCommand<string>(GetPlacesByName);
            GetPlaceDetailCommand = new AsyncRelayCommand<PlaceAutoCompletePrediction>(GetPlaceDetail);
        }


        private async Task OnZoomInAsync()
        {
            zoomLevel += (zoomLevel > 100) ? 50 : 5;
            await ReloadMap();
        }

        private async Task OnZoomOutAsync()
        {
            zoomLevel -= (zoomLevel > 100) ? 20 : 5;
            await ReloadMap();
        }

        private async Task ReloadMap()
        {
            // Implement map reload logic here
            await Task.CompletedTask;
        }

        private async Task OnDoneClickedAsync()
        {
            await Shell.Current.DisplayAlert("Done", "It's done!", "OK");
        }

        private async Task GetPlacesByName(string placeText)
        {
            var placesResult = await _googleMapsApi.GetPlaces(placeText);
            Places = new ObservableCollection<PlaceAutoCompletePrediction>(placesResult.AutoCompletePlaces);
        }

        private async Task GetPlaceDetail(PlaceAutoCompletePrediction place)
        {
            var placeDetail = await _googleMapsApi.GetPlaceDetails(place.PlaceId);
            if (placeDetail != null)
            {
                // Do something with the detailed location
            }
        }
        
        public async Task InitializeAsync(Mapsui.Map map, IEnumerable<Node> nodes, string iconCode)
        {
            Features.Clear();
            map.Layers.Clear();

            var tileLayer = OpenStreetMap.CreateTileLayer();
            map.Layers.Add(tileLayer);

            await LoadMarkersAsync(nodes, iconCode);

            var markerLayer = new Layer("Markers")
            {
                DataSource = new MemoryProvider(Features)
            };

            map.Layers.Add(markerLayer);
        }

        public async Task LoadMarkersAsync(IEnumerable<Node> nodes, string icon)
        {
            Features.Clear();
            foreach (var node in nodes)
            {
                Features.Add(await CreateFeatureAsync(node.Longitude, node.Latitude, icon, "client"));
            }
            await Task.CompletedTask;
        }

        private async Task<IFeature> CreateFeatureAsync(double longitude, double latitude, string icon, string label)
        {
            var feature = new PointFeature(Mapsui.Projections.SphericalMercator.FromLonLat(longitude, latitude).ToMPoint())
            {
                ["Label"] = label
            };

            var bitmapId = await FontAwesomeHelper.CreateFontAwesomePin(icon, "FluentSystemIconsRegular.ttf", SKColors.Red, 64);
            feature.Styles.Add(new SymbolStyle { BitmapId = bitmapId });

            feature.Styles.Add(new LabelStyle
            {
                Text = icon,
                Font = { FontFamily = "FluentIconRegular", Size = 18 },
                BackColor = new Mapsui.Styles.Brush(Mapsui.Styles.Color.Transparent),
                ForeColor = Mapsui.Styles.Color.Red
            });

            return feature;
        }

       
    }
}
