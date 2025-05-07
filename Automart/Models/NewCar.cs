namespace Automart.Models
{
    public class NewCar
    {
        public int NewCarID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarType { get; set; }  // Already included in DB
        public string FuelType { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string? ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
