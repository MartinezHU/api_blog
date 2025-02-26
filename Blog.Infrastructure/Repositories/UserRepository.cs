using Blog.Application.Contracts.Persistence;
using Blog.Domain;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class UserRepository(BlogDbContext context) : RepositoryBase<User>(context), IUserRepository
    {
        public Task<User> GetByAuthUserId(int id)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.AuthUserId == id)!;
        }
    }
}
