using MediatR;

namespace Blog.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public int AuthUserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}

