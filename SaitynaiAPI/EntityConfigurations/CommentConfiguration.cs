using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaitynaiAPI.Entities;

namespace SaitynaiAPI.EntityConfigurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(new Comment
            {
                Id = 1,
                Body = "Cute :)",
                ThreadId = 1,
                UserId = 1
            },
            new Comment
            {
                Id = 2,
                Body = "Fast car go vrooom haha",
                ThreadId = 2,
                UserId = 3
            },
            new Comment
            {
                Id = 3,
                Body = "Hahaha",
                ThreadId = 2,
                UserId = 2
            },
            new Comment
            {
                Id = 4,
                Body = "Damn she's cute",
                ThreadId = 1,
                UserId = 3
            },
            new Comment
            {
                Id = 5,
                Body = "Cutie",
                ThreadId = 1,
                UserId = 2
            },
            new Comment
            {
                Id = 6,
                Body = "True",
                ThreadId = 1,
                UserId = 1
            });
        }
    }
}
