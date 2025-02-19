using AutoMapper;
using Blog.Application.Contracts.Persistence;
using Blog.Application.Exceptions;
using Blog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePostCommandHandler> _logger;

        public UpdatePostCommandHandler(IPostRepository repository, IMapper mapper, ILogger<UpdatePostCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var postToUpdate = await _repository.GetPostById(request.Id);

            if (postToUpdate == null)
            {
                _logger.LogError("Post ID not found {PostId}", request.Id);
                throw new NotFoundException(nameof(Post), request.Id);
            }

            _mapper.Map(request, postToUpdate, typeof(UpdatePostCommand), typeof(Post));

            var postUpdated = await _repository.UpdateAsync(postToUpdate);

            _logger.LogInformation("Post updated successfully: {PostId}", postUpdated.Id);

            return Unit.Value;
        }
    }
}
