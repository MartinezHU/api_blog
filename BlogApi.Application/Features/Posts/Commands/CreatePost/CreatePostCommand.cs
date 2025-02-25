using Blog.Domain.Enums;
using MediatR;

namespace Blog.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public PostStatus Status { get; set; }
    }
}
