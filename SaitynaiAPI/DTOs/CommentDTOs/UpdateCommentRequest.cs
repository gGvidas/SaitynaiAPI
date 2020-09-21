using System.ComponentModel.DataAnnotations;

namespace SaitynaiAPI.DTOs.CommentDTOs
{
    public class UpdateCommentRequest
    {
        [Required(ErrorMessage = "Body is required")]
        [StringLength(10000)]
        public string Body { get; set; }
    }
}
