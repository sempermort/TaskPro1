using TaskPro1.Models;
namespace TaskPro1.Services
{
    public interface IGoogleMapsApiService
    {
        Task<Direction> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude);
        Task<PlaceAutoCompleteResult> GetPlaces(string text);
        Task<Place> GetPlaceDetails(string placeId);
    }
}