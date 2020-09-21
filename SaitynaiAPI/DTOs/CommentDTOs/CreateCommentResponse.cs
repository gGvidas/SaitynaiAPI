namespace SaitynaiAPI.DTOs.CommentDTOs
{
    public class CreateCommentResponse
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public int ThreadId { get; set; }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
