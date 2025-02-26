using FluentValidation;

namespace Blog.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.AuthUserId).NotEmpty().WithMessage("AuthUserId cannot be empty.");

            RuleFor(u => u.IsActive).NotNull().WithMessage(u => $"The user {u.AuthUserId} must be an active user.");

            RuleFor(u => u.Username).NotEmpty().WithMessage("Username cannot be empty");
        }
    }
}
