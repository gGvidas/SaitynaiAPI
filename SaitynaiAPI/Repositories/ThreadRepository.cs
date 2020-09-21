using Microsoft.EntityFrameworkCore;
using SaitynaiAPI.Entities;
using SaitynaiAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynaiAPI.Repositories
{
    public class ThreadRepository : IThreadRepository
    {
        private readonly SaitynaiDbContext _context;
        public ThreadRepository(SaitynaiDbContext context)
        {
            _context = context;
        }
        public Thread Find(int id)
        {
            return _context.Threads.Find(id);
        }
        public async Task<List<Thread>> GetAll()
        {
            return await _context.Threads.ToListAsync();
        }
        public async Task<List<Thread>> GetByCategory(int categoryId)
        {
            return await _context.Threads.Where(thread => thread.CategoryId == categoryId).ToListAsync();
        }
        public void Create(Thread thread)
        {
            _context.Threads.Add(thread);
        }
        public void Delete(Thread thread)
        {
            _context.Threads.Remove(thread);
        }
        public void Update(Thread thread)
        {
            _context.Threads.Update(thread);
        }

    }
}
