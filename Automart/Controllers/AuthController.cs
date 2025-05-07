namespace Automart.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using BCrypt.Net;
    using Automart.Models;

    public class AuthController : Controller
    {
        private readonly CarContext _context;

        public AuthController(CarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View("~/Views/Home/signin.cshtml"); // explicitly reference the correct path
        }

        [HttpPost]
        public IActionResult Signin(string username, string password)
        {
            var user = _context.Auth.FirstOrDefault(a => a.Username == username);

            if (user != null && BCrypt.Verify(password, user.PasswordHash))
            {
                if (user.Role == "User")
                    return RedirectToAction("userhome", "Home");

                if (user.Role == "Dealer")
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View("~/Views/Home/signin.cshtml");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            // 👇 Explicitly refer to Views/Home/signup.cshtml
            return View("~/Views/Home/signup.cshtml");
        }

        [HttpPost]
        public IActionResult Signup(string username, string password, string confirmPassword, string role,
                            string firstname, string lastname, string email, string phone)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View("~/Views/Home/signup.cshtml");
            }

            string hashedPassword = BCrypt.HashPassword(password);

            // Step 1: Insert into Auth (without AuthID yet)
            var auth = new Auth
            {
                Username = username,
                PasswordHash = hashedPassword,
                Role = role,
                CreatedAt = DateTime.Now
            };

            _context.Auth.Add(auth);
            _context.SaveChanges(); // Save to get InternalID assigned

            // Step 2: Generate AuthID based on InternalID
            string prefix = role == "User" ? "U" : "D";
            auth.AuthID = prefix + auth.InternalID.ToString("D6"); // e.g., U000001 or D000003

            _context.SaveChanges(); // Update AuthID

            // Step 3: Insert into respective table using generated AuthID
            if (role == "User")
            {
                var user = new Users
                {
                    UserID = auth.AuthID,
                    FirstName = firstname,
                    LastName = lastname,
                    Phone = phone,
                    Address = email, // or use a separate address field if available
                    CreatedAt = DateTime.Now
                };
                _context.Users.Add(user);
            }
            else if (role == "Dealer")
            {
                var dealer = new Dealers
                {
                    DealerID = auth.AuthID,
                    FirstName = firstname,
                    LastName = lastname,
                    Phone = phone,
                    Address = email,
                    CreatedAt = DateTime.Now
                };
                _context.Dealers.Add(dealer);
            }

            _context.SaveChanges(); // Save Users or Dealers

            return RedirectToAction("Signin", "Auth");
        }

    }
}
