using Microsoft.AspNetCore.Mvc;
using Automart.Models;

namespace Automart.Controllers
{
    [ApiController]
    [Route("CarDelete")]
    public class CarDeleteController : ControllerBase
    {
        private readonly CarContext _context;

        public CarDeleteController(CarContext context)
        {
            _context = context;
        }

        public class DeleteCarRequest
        {
            public int Id { get; set; }
            public bool IsUsed { get; set; }
            public string Reason { get; set; } = string.Empty;
        }

        [HttpPost("DeleteCar")]
        public async Task<IActionResult> DeleteCar([FromBody] DeleteCarRequest request)
        {
            Console.WriteLine($"🔍 Delete Request - ID: {request.Id}, IsUsed: {request.IsUsed}, Reason: {request.Reason}");

            if (request.Id <= 0)
                return BadRequest("Invalid ID.");

            if (request.IsUsed)
            {
                var car = await _context.UsedCars.FindAsync(request.Id);
                if (car == null) return NotFound("Used car not found.");

                _context.UsedCars.Remove(car);
            }
            else
            {
                var car = await _context.NewCars.FindAsync(request.Id);
                if (car == null) return NotFound("New car not found.");

                _context.NewCars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Car deleted successfully" });
        }
    }
}

