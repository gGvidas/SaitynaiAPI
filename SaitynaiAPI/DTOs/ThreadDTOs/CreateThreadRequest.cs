using System.ComponentModel.DataAnnotations;

namespace SaitynaiAPI.DTOs.ThreadDTOs
{
    public class CreateThreadRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(10000)]
        public string Body { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

    }
}
