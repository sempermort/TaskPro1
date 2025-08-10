using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TaskPro1.Models;

namespace TaskPro1.Services
{
    public class GoogleMapsApiService : IGoogleMapsApiService
    {
        static string? _googleMapsKey;

        private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
        public static void Initialize(string googleMapsKey)
        {
            _googleMapsKey = googleMapsKey;
        }

        public async Task<Direction> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        {
            Direction googleDirection = new Direction();

            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={originLatitude},{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        try
                        {
                            var direction = JsonConvert.DeserializeObject<Direction>(json);
                            if (direction != null)
                            {
                                googleDirection = direction;
                            }
                        }
                        catch (JsonException)
                        {
                            // Optionally log the error here                                
                        }
                    }
                }
            }

            return googleDirection;
        }
        public async Task<PlaceAutoCompleteResult> GetPlaces(string text)
        {
            PlaceAutoCompleteResult? results = null;

            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/place/autocomplete/json?input={Uri.EscapeDataString(text)}&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        results = await Task.Run(() =>
                           JsonConvert.DeserializeObject<PlaceAutoCompleteResult>(json)
                        ).ConfigureAwait(false);
                    }
                }
            }

            return results;
        }

        public async Task<Place?> GetPlaceDetails(string placeId)
        {
            Place? result = null;
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/place/details/json?placeid={Uri.EscapeDataString(placeId)}&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        result = new Place(JObject.Parse(json));
                    }
                }
            }

            return result;
        }
    }
}

