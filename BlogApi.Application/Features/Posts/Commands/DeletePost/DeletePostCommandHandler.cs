using Blog.Application.Contracts.Persistence;
using Blog.Application.Exceptions;
using Blog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostRepository _repository;
        private readonly ILogger<DeletePostCommandHandler> _logger;

        public DeletePostCommandHandler(IPostRepository repository, ILogger<DeletePostCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var postToDelete = await _repository.GetByIdAsync(request.Id);
            if (postToDelete == null)
            {
                _logger.LogError("Post with Id {PostId} not found", request.Id);
                throw new NotFoundException(nameof(Post), request.Id);
            }

            await _repository.DeleteAsync(postToDelete);

            _logger.LogInformation("Post with Id {PostId} deleted successfully", postToDelete.Id);

            return Unit.Value;
        }
    }
}
