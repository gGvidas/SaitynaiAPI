using System.ComponentModel.DataAnnotations;

namespace SaitynaiAPI.DTOs.ThreadDTOs
{
    public class UpdateThreadRequest
    {
        [StringLength(10000)]
        public string Body { get; set; }
    }
}
