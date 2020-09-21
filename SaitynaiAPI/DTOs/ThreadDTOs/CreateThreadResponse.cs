using SaitynaiAPI.DTOs.CommentDTOs;
using System.Collections.Generic;

namespace SaitynaiAPI.DTOs.ThreadDTOs
{
    public class CreateThreadResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public ICollection<GetCommentResponse> Comments { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public int CategoryId { get; set; }
    }
}
