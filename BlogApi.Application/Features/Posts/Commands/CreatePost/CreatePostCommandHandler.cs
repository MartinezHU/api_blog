using AutoMapper;
using Blog.Application.Contracts.Persistence;
using Blog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePostCommandHandler> _logger;

        public CreatePostCommandHandler(IPostRepository repository, IMapper mapper, ILogger<CreatePostCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var postEntity = _mapper.Map<Post>(request);
            var newPost = await _repository.AddAsync(postEntity);

            _logger.LogInformation("Post {PostID} created successfully", newPost.Id);

            return newPost.Id;
        }
    }
}
