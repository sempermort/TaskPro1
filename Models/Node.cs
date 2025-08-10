namespace TaskPro1.Models
{
    public class Node
    {
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Details { get; set; }
        public string? Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal Price { get; set; }
        public required string Department { get; set; }
    }
}