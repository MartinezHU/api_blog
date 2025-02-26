using Blog.Domain;

namespace Blog.Application.Contracts.Persistence
{
    public interface IUserRepository : IASyncRepository<User>
    {
        public Task<User> GetByAuthUserId(int id);
    }
}

