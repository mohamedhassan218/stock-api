using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return comment;
            }
            return null;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(c => c.UserNavigation).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(c => c.UserNavigation)
                .FirstOrDefaultAsync(c => c.Id == id);
            return (comment == null) ? null : comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment != null)
            {
                comment.Title = commentModel.Title;
                comment.Content = commentModel.Content;
                await _context.SaveChangesAsync();
                return comment;
            }
            return null;
        }
    }
}