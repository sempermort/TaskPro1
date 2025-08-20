using TaskPro1.Models;

namespace TaskPro1.Helpers
{
    public interface IMapOperationsService
    {
        void CenterMapOnLocation(double lon, double lat, double zoom);
        Task AnimateTapAsync(Image image);
        Task LoadMarkersAsync(IEnumerable<Node> nodes, string icon);
        void RefreshMap();
    }
}