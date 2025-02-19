using Blog.Domain.Common;

namespace Blog.Domain
{
    public class Comment : BaseDomainModel
    {
        public required string Content { get; set; }
        public int PostId { get; set; }
        public required Post Post { get; set; }
        public int AuthorId { get; set; }
        public required User Author { get; set; }
    }
}
