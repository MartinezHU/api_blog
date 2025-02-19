using Blog.Domain;

namespace Blog.Application.Contracts.Persistence
{
    public interface IAuthorRepository : IASyncRepository<User>
    {
    }
}
