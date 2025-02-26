using AutoMapper;
using Blog.Application.Contracts.Persistence;
using Blog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository repository, IMapper mapper, ILogger<CreateUserCommandHandler> logger) : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger = logger;

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<User>(request);

            var newUser = await _userRepository.AddAsync(userEntity);

            _logger.LogInformation("User {UserID} created successfully", newUser.Id);

            return newUser.Id;
        }
    }
}
