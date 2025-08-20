using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Tiling;
using SkiaSharp;
using System.Collections.ObjectModel;
using TaskPro1.Helpers;
using TaskPro1.Models;

namespace TaskPro1.Controls;

public partial class MapViewControl : ContentView
{
    public static readonly BindableProperty ZoomLevelProperty = 
        BindableProperty.Create(nameof(ZoomLevel), typeof(double), typeof(MapViewControl), 16.0, 
            propertyChanged: OnZoomLevelChanged);

    public static readonly BindableProperty CenterProperty = 
        BindableProperty.Create(nameof(Center), typeof(Position), typeof(MapViewControl), new Position(0, 0),
            propertyChanged: OnCenterChanged);

    public static readonly BindableProperty MarkersProperty = 
        BindableProperty.Create(nameof(Markers), typeof(ObservableCollection<Node>), typeof(MapViewControl), new ObservableCollection<Node>(),
            propertyChanged: OnMarkersChanged);

    public double ZoomLevel
    {
        get => (double)GetValue(ZoomLevelProperty);
        set => SetValue(ZoomLevelProperty, value);
    }

    public Position Center
    {
        get => (Position)GetValue(CenterProperty);
        set => SetValue(CenterProperty, value);
    }

    public ObservableCollection<Node> Markers
    {
        get => (ObservableCollection<Node>)GetValue(MarkersProperty);
        set => SetValue(MarkersProperty, value);
    }

    public MapViewControl()
    {
        InitializeComponent();
        InitializeMap();
    }

    private void InitializeMap()
    {
        if (mapControl.Map == null)
            mapControl.Map = new Mapsui.Map();

        var tileLayer = OpenStreetMap.CreateTileLayer();
        mapControl.Map.Layers.Add(tileLayer);
    }

    private static void OnZoomLevelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MapViewControl)bindable;
        control.mapControl.Map.Navigator.ZoomTo((double)newValue);
    }

    private static void OnCenterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MapViewControl)bindable;
        var position = (Position)newValue;
        var sm = SphericalMercator.FromLonLat(position.Longitude, position.Latitude);
        control.mapControl.Map.Navigator.CenterOn(sm.ToMPoint());
    }

    private static void OnMarkersChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MapViewControl)bindable;
        control.UpdateMarkers();
    }

    private void UpdateMarkers()
    {
        // Remove existing marker layer if it exists
        var markerLayer = mapControl.Map.Layers.FirstOrDefault(l => l.Name == "Markers");
        if (markerLayer != null)
            mapControl.Map.Layers.Remove(markerLayer);

        // Create new marker layer
        var features = new Collection<IFeature>();
        foreach (var node in Markers)
        {
            var feature = new PointFeature(SphericalMercator.FromLonLat(node.Longitude, node.Latitude).ToMPoint());
            features.Add(feature);
        }

        markerLayer = new Layer("Markers")
        {
            DataSource = new MemoryProvider(features)
        };

        mapControl.Map.Layers.Add(markerLayer);
    }

    private void OnZoomInClicked(object sender, EventArgs e)
    {
        ZoomLevel += 1;
    }

    private void OnZoomOutClicked(object sender, EventArgs e)
    {
        ZoomLevel -= 1;
    }

    public void CenterOnLocation(double longitude, double latitude)
    {
        Center = new Position(longitude, latitude);
    }

    public void RefreshMap()
    {
        mapControl.Refresh();
    }
}

public struct Position
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    public Position(double longitude, double latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}