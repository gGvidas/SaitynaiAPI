using SaitynaiAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaitynaiAPI.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Comment Find(int id);
        Task<List<Comment>> GetByThread(int threadId);
        Task<List<Comment>> GetByUser(int userId);
        void Create(Comment comment);
        void Delete(Comment comment);
        void Update(Comment comment);
    }
}
