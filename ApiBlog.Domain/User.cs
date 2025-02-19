using Blog.Domain.Common;

namespace Blog.Domain
{
    public class User : BaseDomainModel
    {

        public required string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsStaff { get; set; }
        public string? Username { get; set; }
        public required string OriginApp { get; set; }

        public ICollection<Role> Roles { get; set; } = [];

        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<PostHistory> PostHistories { get; set; } = [];
    }
}
