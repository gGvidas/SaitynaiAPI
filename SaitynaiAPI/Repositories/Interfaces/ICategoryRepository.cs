using SaitynaiAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaitynaiAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Category Find(int id);
        Task<List<Category>> GetAll();
        void Create(Category category);
        void Delete(Category category);
        void Update(Category category);

    }
}
