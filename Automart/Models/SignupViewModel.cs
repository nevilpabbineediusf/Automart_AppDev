namespace Automart.Models
{
    public class SignupViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "User" or "Dealer"

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

}
