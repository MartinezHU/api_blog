using AutoMapper;
using Blog.Application.Contracts.Persistence;
using Blog.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePostCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreatePostCommandHandler(IPostRepository repository, IMapper mapper, ILogger<CreatePostCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("user_id")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("The user ID was not found in the token.");
            }

            if (!int.TryParse(userId, out int authorId))
            {
                throw new InvalidOperationException("The user ID in the token is not a valid integer.");
            }

            var postEntity = _mapper.Map<Post>(request);
            postEntity.AuthorId = authorId;
            var newPost = await _repository.AddAsync(postEntity);

            _logger.LogInformation("Post {PostID} created successfully", newPost.Id);

            return newPost.Id;
        }
    }
}
