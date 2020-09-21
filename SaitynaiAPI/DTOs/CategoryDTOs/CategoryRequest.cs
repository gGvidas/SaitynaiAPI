using System.ComponentModel.DataAnnotations;

namespace SaitynaiAPI.DTOs.CategoryDTOs
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
