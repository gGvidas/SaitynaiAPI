using SaitynaiAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaitynaiAPI.Repositories.Interfaces
{
    public interface IThreadRepository
    {
        Thread Find(int id);
        Task<List<Thread>> GetAll();
        Task<List<Thread>> GetByCategory(int categoryId);
        void Create(Thread thread);
        void Delete(Thread thread);
        void Update(Thread thread);
    }
}
