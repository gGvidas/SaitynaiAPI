using System.ComponentModel.DataAnnotations;

namespace SaitynaiAPI.DTOs.CommentDTOs
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage = "Body is required")]
        [StringLength(10000)]
        public string Body { get; set; }

        [Required(ErrorMessage = "Thread is required")]
        public int ThreadId { get; set; }
    }
}
