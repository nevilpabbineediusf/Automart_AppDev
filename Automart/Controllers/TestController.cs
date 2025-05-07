namespace Automart.Controllers
{
    using Automart.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class TestController : Controller
    {
        private readonly CarContext _context;

        public TestController(CarContext context)
        {
            _context = context;
        }

        // GET: /Test/CheckDb
        public IActionResult CheckDb()
        {
            try
            {
                // Try to fetch any records from the Auth table
                var auths = _context.Auth.ToList();

                return Content($"Connected successfully! Found {auths.Count} record(s) in the Auth table.");
            }
            catch (Exception ex)
            {
                return Content("Database connection failed: " + ex.Message);
            }
        }
    }

}
