namespace Automart.Models
{
    public class CarUpdateDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarType { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public bool IsUsed { get; set; }
    }
}

