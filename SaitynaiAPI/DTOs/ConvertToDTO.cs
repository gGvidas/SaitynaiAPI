using SaitynaiAPI.DTOs.CategoryDTOs;
using SaitynaiAPI.DTOs.CommentDTOs;
using SaitynaiAPI.DTOs.ThreadDTOs;
using SaitynaiAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SaitynaiAPI.DTOs
{
    public static class ConvertToDTO
    {
        public static GetCategoryResponse ToGetResponse(Category category)
        {
            return new GetCategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Threads = ToGetResponse(category.Threads)
            };
        }
        public static ICollection<GetCategoryCollectionResponse>  ToGetResponse(ICollection<Category> categories)
        {
            return categories?.Select(category => new GetCategoryCollectionResponse
                {
                    Id = category.Id,
                    Name = category.Name
                }).ToList();
        }
        public static CreateCategoryResponse ToCreateResponse(Category category)
        {
            return new CreateCategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public static GetCommentResponse ToGetResponse(Comment comment)
        {
            return new GetCommentResponse
            {
                Id = comment.Id,
                Body = comment.Body,
                ThreadId = comment.ThreadId,
                UserEmail = comment.User.Email,
                UserId = comment.UserId
            };
        }
        public static ICollection<GetCommentResponse> ToGetResponse(ICollection<Comment> comments)
        {
            return comments?.Select(comment => ToGetResponse(comment)).ToList();
        }
        public static CreateCommentResponse ToCreateResponse(Comment comment)
        {
            return new CreateCommentResponse
            {
                Id = comment.Id,
                Body = comment.Body,
                ThreadId = comment.ThreadId,
                UserEmail = "",
                UserId = comment.UserId
            };
        }
        public static GetThreadResponse ToGetResponse(Thread thread)
        {
            return new GetThreadResponse
            {
                Id = thread.Id,
                Title = thread.Title,
                Body = thread.Body,
                CategoryId = thread.CategoryId,
                Comments = ToGetResponse(thread.Comments),
                UserEmail = thread.User.Email,
                UserId = thread.UserId
            };
        }
        public static ICollection<GetThreadCollectionResponse> ToGetResponse(ICollection<Thread> threads)
        {
            return threads?.Select(thread => new GetThreadCollectionResponse
                {
                    Id = thread.Id,
                    Body = thread.Body,
                    Title = thread.Title,
                    CategoryId = thread.CategoryId,
                    UserEmail = thread.User.Email,
                    UserId = thread.UserId
                }).ToList();
        }
        public static CreateThreadResponse ToCreateResponse(Thread thread)
        {
            return new CreateThreadResponse
            {
                Id = thread.Id,
                Title = thread.Title,
                Body = thread.Body,
                CategoryId = thread.CategoryId,
                Comments = ToGetResponse(thread.Comments),
                UserEmail = "",
                UserId = thread.UserId
            };
        }
    }
}
