using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaitynaiAPI.Entities;

namespace SaitynaiAPI.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasMany(user => user.Threads)
                .WithOne(thread => thread.User)
                .HasForeignKey(thread => thread.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.Comments)
                .WithOne(comment => comment.User)
                .HasForeignKey(comment => comment.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new User
            {
                Id = 1,
                Email = "admin@admin.com",
                Password = "admin",
                isAdmin = true
            },
            new User
            {
                Id = 2,
                Email = "user1@user.com",
                Password = "user1",
                isAdmin = false
            },
            new User
            {
                Id = 3,
                Email = "user2@user.com",
                Password = "user2",
                isAdmin = false
            });
        }
    }
}
