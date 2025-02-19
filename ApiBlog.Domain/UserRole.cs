using Blog.Domain.Common;

namespace Blog.Domain
{
    public class UserRole : BaseDomainModel
    {
        public int UserId { get; set; }
        public required User User { get; set; }
        public int RoleId { get; set; }
        public required Role Role { get; set; }
    }
}
