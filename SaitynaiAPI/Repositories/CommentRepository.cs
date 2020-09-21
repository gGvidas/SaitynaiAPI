using Microsoft.EntityFrameworkCore;
using SaitynaiAPI.Entities;
using SaitynaiAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynaiAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SaitynaiDbContext _context;
        public CommentRepository(SaitynaiDbContext context)
        {
            _context = context;
        }

        public Comment Find(int id)
        {
            return _context.Comments.Find(id);
        }
        public async Task<List<Comment>> GetByThread(int threadId)
        {
            return await _context.Comments.Where(comment => comment.ThreadId == threadId).ToListAsync();
        }
        public async Task<List<Comment>> GetByUser(int userId)
        {
            return await _context.Comments.Where(comment => comment.UserId == userId).ToListAsync();
        }

        public void Create(Comment comment)
        {
            _context.Comments.Add(comment);
        }
        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
        }
        public void Update(Comment comment)
        {
            _context.Update(comment);
        }
    }
}
