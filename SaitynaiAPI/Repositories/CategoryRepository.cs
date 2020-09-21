using Microsoft.EntityFrameworkCore;
using SaitynaiAPI.Entities;
using SaitynaiAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaitynaiAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SaitynaiDbContext _context;
        public CategoryRepository(SaitynaiDbContext context)
        {
            _context = context;
        }
        public Category Find(int id)
        {
            return _context.Categories.Find(id);
        }
        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }
        public void Create(Category category)
        {
            _context.Add(category);
        }
        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
        public void Update(Category category)
        {
            _context.Update(category);
        }
    }
}
