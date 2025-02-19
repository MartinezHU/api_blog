using Blog.Domain;
namespace Blog.Application.Contracts.Persistence
{
    public interface IPostRepository : IASyncRepository<Post>
    {
        Task<Post> GetPostById(int postId);
        Task<IEnumerable<Post>> GetPostsByAuthor(int authorId);
    }
}
