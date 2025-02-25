using FluentValidation;

namespace Blog.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{Title} cannot be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("{Title} cannot contain more than 50 characters");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("{Content} cannot be empty");

            RuleFor(p => p.Status)
                .NotNull().WithMessage("{Status} cannot be null");
        }
    }
}
