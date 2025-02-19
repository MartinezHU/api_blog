using Blog.Domain.Common;

namespace Blog.Domain
{
    public class Role : BaseDomainModel
    {
        public required string Name { get; set; }
        public List<UserRole> UserRoles { get; set; } = [];
    }
}
