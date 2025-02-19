using Blog.Domain.Common;

namespace Blog.Domain
{
    public class PostTag : BaseDomainModel
    {
        public int PostId { get; set; }
        public required Post Post { get; set; }
        public int TagId { get; set; }
        public required Tag Tag { get; set; }
    }
}
