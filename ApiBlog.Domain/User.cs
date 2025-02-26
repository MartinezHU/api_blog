using Blog.Domain.Common;

namespace Blog.Domain
{
    public class User : BaseDomainModel
    {

        public int AuthUserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostHistory>? PostHistories { get; set; }
    }
}
