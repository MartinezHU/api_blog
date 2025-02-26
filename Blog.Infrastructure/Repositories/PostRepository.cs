using Blog.Application.Contracts.Persistence;
using Blog.Domain;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class PostRepository(BlogDbContext context) : RepositoryBase<Post>(context), IPostRepository
    {
        public async Task<Post> GetPostById(int postId)
        {
            return await _context.Posts!.Where(p => p.Id == postId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthor(int authorId)
        {
            return await _context.Posts!.Where(a => a.Id == authorId).ToListAsync();
        }
    }
}
