namespace TaskPro1.Models
{
    public class PriceOption
    {
        public required string Category { get; set; }
        public required string CategoryDescription { get; set; }
        public string? Tag { get; set; }
        public List<PriceDetail>? PriceDetails { get; set; }

    }
}