using Microsoft.AspNetCore.Mvc;
using Automart.Models;
using System.Threading.Tasks;

namespace Automart.Controllers
{
    [ApiController]
    [Route("CarUpdate")] // This MUST match the frontend URL: /CarUpdate/UpdateCar
    public class CarUpdateController : ControllerBase
    {
        private readonly CarContext _context;

        public CarUpdateController(CarContext context)
        {
            _context = context;
        }

        [HttpPost("UpdateCar")]
        public async Task<IActionResult> UpdateCar([FromBody] CarUpdateDto car)
        {
            if (car == null)
                return BadRequest("Invalid car data.");

            if (car.IsUsed)
            {
                var existing = await _context.UsedCars.FindAsync(car.Id);
                if (existing == null) return NotFound("Used car not found.");

                existing.Brand = car.Brand;
                existing.Model = car.Model;
                existing.Year = car.Year;
                existing.CarType = car.CarType;
                existing.FuelType = car.FuelType;
                existing.Mileage = car.Mileage;
                existing.Price = car.Price;
                existing.Address = car.Address;
            }
            else
            {
                var existing = await _context.NewCars.FindAsync(car.Id);
                if (existing == null) return NotFound("New car not found.");

                existing.Brand = car.Brand;
                existing.Model = car.Model;
                existing.Year = car.Year;
                existing.CarType = car.CarType;
                existing.FuelType = car.FuelType;
                existing.Price = car.Price;
                existing.Address = car.Address;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Car updated successfully" });
        }
    }
}
