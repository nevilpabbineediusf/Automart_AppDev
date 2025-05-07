namespace Automart.Models
{
    public class CarViewModel
    {
        public string Condition { get; set; } // "New" or "Used"
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarType { get; set; }
        public string FuelType { get; set; }
        public decimal Price { get; set; }
        public int? Mileage { get; set; }
        public string? OwnerId { get; set; }
        public string? Address { get; set; }
        public List<string> ImageUrls { get; set; }
    }


}
