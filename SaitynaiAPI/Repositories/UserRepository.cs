using Microsoft.EntityFrameworkCore;
using SaitynaiAPI.DTOs.UserDTOs;
using SaitynaiAPI.Entities;
using System.Threading.Tasks;

namespace SaitynaiAPI.Repositories.Interfaces
{
    public class UserRepository : IUserRepository
    {
        private readonly SaitynaiDbContext _context;
        public UserRepository(SaitynaiDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }
        public async Task<User> GetUser(LoginRequest request)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == request.Email && user.Password == request.Password);
        }
        public void Create(User user)
        {
            _context.Users.Add(user);
        }
        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
