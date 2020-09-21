using Microsoft.EntityFrameworkCore;
using SaitynaiAPI.Entities;
using System.Reflection;

namespace SaitynaiAPI
{
    public class SaitynaiDbContext : DbContext
    {
        public SaitynaiDbContext(DbContextOptions<SaitynaiDbContext> options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
