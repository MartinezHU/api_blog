using FluentValidation;
using MediatR;
using ValidationException = Blog.Application.Exceptions.ValidationException;

namespace Blog.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _valiators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> valiators)
        {
            _valiators = valiators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_valiators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validations = await Task.WhenAll(_valiators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validations.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }

            }

            return await next();
        }

    }
}
