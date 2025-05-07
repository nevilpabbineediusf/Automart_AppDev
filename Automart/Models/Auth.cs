namespace Automart.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Auth
    {
        [Key]
        public int InternalID { get; set; }  // Primary key in table

        [Required]
        public string AuthID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }


}
