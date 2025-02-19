namespace Blog.Application.Models
{
    public class Email
    {
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
