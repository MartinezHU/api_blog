using Blog.Domain.Common;

namespace Blog.Domain
{
    public class Tag : BaseDomainModel
    {
        public required string Name { get; set; }
        public List<PostTag> PostTags { get; set; } = [];
    }
}
