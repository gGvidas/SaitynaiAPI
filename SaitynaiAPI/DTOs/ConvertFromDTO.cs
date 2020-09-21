using SaitynaiAPI.DTOs.CategoryDTOs;
using SaitynaiAPI.DTOs.CommentDTOs;
using SaitynaiAPI.DTOs.ThreadDTOs;
using SaitynaiAPI.DTOs.UserDTOs;
using SaitynaiAPI.Entities;

namespace SaitynaiAPI.DTOs
{
    public static class ConvertFromDTO
    {
        public static Category FromCreateRequest(CategoryRequest request)
        {
            return new Category
            {
                Name = request.Name
            };
        }
        public static Comment FromCreateRequest(CreateCommentRequest request)
        {
            return new Comment
            {
                Body = request.Body,
                ThreadId = request.ThreadId,
            };
        }
        public static Thread FromCreateRequest(CreateThreadRequest request)
        {
            return new Thread
            {
                Title = request.Title,
                Body = request.Body,
                CategoryId = request.CategoryId,
            };
        }
        public static User FromCreateRequest(LoginRequest request)
        {
            return new User
            {
                Email = request.Email,
                Password = request.Password
            };
        }
    }
}
