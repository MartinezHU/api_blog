using Blog.Domain.Common;

namespace Blog.Domain
{
    public class PostHistory : BaseDomainModel
    {
        public int PostId { get; set; }
        public required Post Post { get; set; }
        public string? Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int AuthorId { get; set; }
        public required User Author { get; set; }
    }
}
