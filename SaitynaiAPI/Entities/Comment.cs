using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaitynaiAPI.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Body is required")]
        [StringLength(10000)]
        public string Body { get; set; }

        [Required(ErrorMessage = "Thread is required")]
        public int ThreadId { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }
        public virtual Thread Thread { get; set; }
        public virtual User User { get; set; }
    }
}
