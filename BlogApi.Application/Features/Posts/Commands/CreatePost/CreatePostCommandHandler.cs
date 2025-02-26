using AutoMapper;
using Blog.Application.Contracts.Persistence;
using Blog.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler(IPostRepository repository, IUserRepository userRepository, IMapper mapper, ILogger<CreatePostCommandHandler> logger, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IPostRepository _repository = repository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CreatePostCommandHandler> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var authUserId = _httpContextAccessor.HttpContext?.User.FindFirst("user_id")?.Value;

            if (string.IsNullOrEmpty(authUserId))
            {
                throw new UnauthorizedAccessException("The user ID was not found in the token.");
            }

            User localUser = await _userRepository.GetByAuthUserId(int.Parse(authUserId))
                ?? throw new UnauthorizedAccessException("User not authorized");

            var postEntity = _mapper.Map<Post>(request);
            postEntity.AuthorId = localUser.Id;
            var newPost = await _repository.AddAsync(postEntity);

            _logger.LogInformation("Post {PostID} created successfully", newPost.Id);

            return newPost.Id;
        }
    }
}
