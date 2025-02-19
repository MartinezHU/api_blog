using Blog.Domain;
using Blog.Domain.Enums;

namespace Blog.Application.DTOs
{
    class PostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public User? Author { get; set; }
        public PostStatus Status { get; set; }
    }
}
