using System.ComponentModel.DataAnnotations;

namespace SaitynaiAPI.DTOs.UserDTOs
{
    public class RefreshRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
