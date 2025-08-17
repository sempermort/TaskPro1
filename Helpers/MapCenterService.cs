using CommunityToolkit.Mvvm.Messaging;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.UI.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro1.ViewModels;

namespace TaskPro1.Helpers
{
    class CenterOnMapService : ICenterOnMap
    {

        private readonly MapView _mapView;
        private readonly Image _image;

        public CenterOnMapService(MapView mapView,Image image)
        {
            _mapView = mapView;
            _image = image;
        }


        public void CenterMapOnLocation(double lon, double lat, double zoom)
        {
            var sm = SphericalMercator.FromLonLat(lon, lat);
            _mapView.Map.Navigator.CenterOn(sm.ToMPoint());
            _mapView.Map.Navigator.ZoomTo(zoom);
            _mapView.Refresh();
        }
        private async void AnimateTapAsync(Image image)
        {
            if (image == null)
                return;

            // 👇 Animate image (pulse effect)
            await _image.ScaleTo(0.8, 100, Easing.CubicOut);
            await _image.ScaleTo(1, 100, Easing.CubicIn);
        }
    }
}
