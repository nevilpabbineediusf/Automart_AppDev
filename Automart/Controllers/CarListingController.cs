using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Automart.Models;

namespace Automart.Controllers
{
    public class CarListingController : Controller
    {
        private readonly CarContext _context;

        public CarListingController(CarContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("CarListing/GetAllCars")]
        public async Task<IActionResult> GetAllCars()
        {
            var newCars = await _context.NewCars
                .Select(c => new
                {
                    NewCarID = c.NewCarID, // ✅ include ID
                    c.Brand,
                    c.Model,
                    c.Year,
                    c.CarType,
                    c.FuelType,
                    Mileage = (int?)null, // New cars don't have mileage
                    c.Price,
                    c.Address,
                    Photos = new List<string> { c.ImageURL ?? "" },
                    CarTypeLabel = "New Car"
                })
                .ToListAsync();

            var usedCars = await _context.UsedCars
                .Select(c => new
                {
                    UsedCarID = c.UsedCarID, // ✅ include ID
                    c.Brand,
                    c.Model,
                    c.Year,
                    c.CarType,
                    c.FuelType,
                    c.Mileage,
                    c.Price,
                    c.Address,
                    Photos = new List<string> { c.ImageURL ?? "" },
                    CarTypeLabel = "Used Car"
                })
                .ToListAsync();

            var allCars = newCars.Concat<object>(usedCars).ToList();
            return Json(allCars);
        }
    }
}
