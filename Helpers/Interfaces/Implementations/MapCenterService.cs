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
using TaskPro1.Helpers.Interfaces;
using TaskPro1.Models;
using TaskPro1.ViewModels;

namespace TaskPro1.Helpers.Interfaces.Implementations
{
    public class MapCenterService :  ICenterOnMap
    {
        private readonly MapControl _mapControl;
        private readonly Image _image;

        public MapCenterService(MapControl mapControl)
        {
            _mapControl = mapControl;
        }

        public void CenterMapOnLocation(double lon, double lat, double zoom)
        {
            var sm = SphericalMercator.FromLonLat(lon, lat);
            _mapControl.Map.Navigator.CenterOn(sm.ToMPoint());
            _mapControl.Map.Navigator.ZoomTo(zoom);
            _mapControl.Refresh();
        }

 

    }
}
