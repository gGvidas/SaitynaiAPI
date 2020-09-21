using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaitynaiAPI.Entities;

namespace SaitynaiAPI.EntityConfigurations
{
    public class ThreadConfiguration : IEntityTypeConfiguration<Thread>
    {
        public void Configure(EntityTypeBuilder<Thread> builder)
        {
            builder.HasMany(thread => thread.Comments)
                .WithOne(comment => comment.Thread)
                .HasForeignKey(comment => comment.ThreadId);

            builder.HasOne(thread => thread.Category)
                .WithMany(category => category.Threads)
                .HasForeignKey(thread => thread.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new Thread
            {
                Id = 1,
                Title = "I have a cute cat",
                Body = "Look at her",
                CategoryId = 1,
                UserId = 2
            },
            new Thread
            {
                Id = 2,
                Title = "Fast car vroom vroom",
                Body = "Vrooom",
                CategoryId = 2,
                UserId = 2
            },
            new Thread
            {
                Id = 3,
                Title = "Slow car :(",
                Body = "brrrrr",
                CategoryId = 2,
                UserId = 3
            });
        }
    }
}
