using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro1.Models;

namespace TaskPro1.Helpers.Interfaces
{
   public interface ICenterOnMap
    {
        void CenterMapOnLocation(double lon, double lat, double zoom);
      
    }
}
