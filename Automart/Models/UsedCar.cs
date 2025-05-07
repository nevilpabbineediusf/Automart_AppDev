namespace Automart.Models
{
    public class UsedCar
    {
        public int UsedCarID { get; set; }
        public string? OwnerID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarType { get; set; }
        public string FuelType { get; set; }
        public decimal Price { get; set; }
        public int? Mileage { get; set; }
        public string? Address { get; set; }
        public string? ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
