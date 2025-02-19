using FluentValidation;

namespace Blog.Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{Title} cannot be empty");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("{Content} cannot be empty");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("{Status} cannot be null");
        }
    }
}
