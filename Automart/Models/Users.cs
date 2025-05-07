using System.ComponentModel.DataAnnotations;

namespace Automart.Models
{
    public class Users
    {
        [Key]
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
