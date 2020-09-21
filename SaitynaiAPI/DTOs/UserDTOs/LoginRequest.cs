using System.ComponentModel.DataAnnotations;

namespace SaitynaiAPI.DTOs.UserDTOs
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
