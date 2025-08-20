using CommunityToolkit.Mvvm.Messaging;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.UI.Maui;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro1.Models;
using TaskPro1.ViewModels;

namespace TaskPro1.Helpers
{
    public class MapOperationsService :  ICenterOnMap
    {
        private readonly MapControl _mapControl;
        private readonly Image _image;

        public MapOperationsService(MapControl mapControl, Image image)
        {
            _mapControl = mapControl;
            _image = image;
        }

        public void CenterMapOnLocation(double lon, double lat, double zoom)
        {
            var sm = SphericalMercator.FromLonLat(lon, lat);
            _mapControl.Map.Navigator.CenterOn(sm.ToMPoint());
            _mapControl.Map.Navigator.ZoomTo(zoom);
            _mapControl.Refresh();
        }

        public async Task AnimateTapAsync(Image image)
        {
            if (image == null)
                return;

            // 👇 Animate image (pulse effect)
            await image.ScaleTo(0.8, 100, Easing.CubicOut);
            await image.ScaleTo(1, 100, Easing.CubicIn);
        }

    }
}
