using Automart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Automart.Controllers
{
    [Route("[controller]")]
    public class CarController : Controller
    {
        private readonly IConfiguration _configuration;

        public CarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Home/Create.cshtml"); // Assumes you have Views/Car/Create.cshtml

        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CarViewModel car)
        {
            try
            {
                string connStr = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string imageUrl = car.ImageUrls?.FirstOrDefault();

                    if (car.Condition == "Used")
                    {
                        var cmd = new SqlCommand(@"
                    INSERT INTO UsedCars (OwnerID, Brand, Model, Year, CarType, FuelType, Price, Mileage, Address, ImageURL, CreatedAt)
                    VALUES (@OwnerID, @Brand, @Model, @Year, @CarType, @FuelType, @Price, @Mileage, @Address, @ImageURL, @CreatedAt)", conn);

                        cmd.Parameters.AddWithValue("@OwnerID", car.OwnerId ?? "U0001");
                        cmd.Parameters.AddWithValue("@Brand", car.Brand);
                        cmd.Parameters.AddWithValue("@Model", car.Model);
                        cmd.Parameters.AddWithValue("@Year", car.Year);
                        cmd.Parameters.AddWithValue("@CarType", car.CarType);
                        cmd.Parameters.AddWithValue("@FuelType", car.FuelType);
                        cmd.Parameters.AddWithValue("@Price", car.Price);
                        cmd.Parameters.AddWithValue("@Mileage", car.Mileage ?? 0);
                        cmd.Parameters.AddWithValue("@Address", car.Address ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ImageURL", imageUrl ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        var cmd = new SqlCommand(@"
                    INSERT INTO NewCars (Brand, Model, Year, CarType, FuelType, Price, Address, ImageURL, CreatedAt)
                    VALUES (@Brand, @Model, @Year, @CarType, @FuelType, @Price, @Address, @ImageURL, @CreatedAt)", conn);

                        cmd.Parameters.AddWithValue("@Brand", car.Brand);
                        cmd.Parameters.AddWithValue("@Model", car.Model);
                        cmd.Parameters.AddWithValue("@Year", car.Year);
                        cmd.Parameters.AddWithValue("@CarType", car.CarType);
                        cmd.Parameters.AddWithValue("@FuelType", car.FuelType);
                        cmd.Parameters.AddWithValue("@Price", car.Price);
                        cmd.Parameters.AddWithValue("@Address", car.Address ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ImageURL", imageUrl ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok(new { message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // This sends the actual error to the frontend
            }
        }

    }
}

