using SaitynaiAPI.DTOs.UserDTOs;
using SaitynaiAPI.Entities;
using System.Threading.Tasks;

namespace SaitynaiAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUser(string email);
        public Task<User> GetUser(LoginRequest request);
        public void Create(User user);
        public void Update(User user);
    }
}
