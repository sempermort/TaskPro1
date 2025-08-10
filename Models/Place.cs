using Newtonsoft.Json.Linq;

namespace TaskPro1.Models
{
    public class Place
    {
        public string? Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Raw { get; set; }

        public Place(JObject jsonObject)
        {
            if (jsonObject is not null)
            {
                Name = (string?)jsonObject?["result"]["name"];
                Latitude = (double)jsonObject?["result"]["geometry"]["location"]["lat"];
                Longitude = (double)jsonObject?["result"]["geometry"]["location"]["lng"];
                Raw = jsonObject.ToString();

            }
                
        }
    }
}