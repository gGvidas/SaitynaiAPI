using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaitynaiAPI.Entities;

namespace SaitynaiAPI.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                Id = 1,
                Name = "Animals"
            },
            new Category
            {
                Id = 2,
                Name = "Cars"
            },
            new Category
            {
                Id = 3,
                Name = "Games"
            });
        }
    }
}
