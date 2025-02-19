using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetPostsList
{
    // Se utiliza record en lugar de class porque es inmutable
    public record GetAllPostsQuery() : IRequest<List<PostVm>>;
}
