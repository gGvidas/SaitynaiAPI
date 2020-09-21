using Microsoft.Extensions.DependencyInjection;
using SaitynaiAPI.Repositories;
using SaitynaiAPI.Repositories.Interfaces;

namespace SaitynaiAPI.StartupConfigurations
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IThreadRepository, ThreadRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
