using Blog.Domain.Common;
using Blog.Domain.Enums;

namespace Blog.Domain
{
    public class Post : BaseDomainModel
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public int AuthorId { get; set; }
        public required User Author { get; set; }
        public PostStatus Status { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostHistory>? PostHistories { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
    }
}