using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPro1.Helpers
{
   public interface ICenterOnMap
    {
        void CenterMapOnLocation(double lon, double lat, double zoom);
        Task AnimateTapAsync(Image image); // Uncomment if you need to animate an image

    }
}
