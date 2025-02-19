using Blog.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Blog.Application.Authorization
{

    public class PostOwnerRequirement : IAuthorizationRequirement { }
    public class CommentModeratorRequirement : IAuthorizationRequirement { }
    public class PostOwnerHandlerRequirement : AuthorizationHandler<PostOwnerRequirement, Post>
    {
        private readonly ILogger<PostOwnerHandlerRequirement> _logger;

        public PostOwnerHandlerRequirement(ILogger<PostOwnerHandlerRequirement> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PostOwnerRequirement requirement,
            Post resource)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                _logger.LogWarning("No se pudo obtener el ID del usuario durante la autorización");
                return Task.CompletedTask;
            }

            if (resource.AuthorId.ToString() == userId)
            {
                _logger.LogInformation("Usuario {UserId} autorizado como propietario del post {PostId}",
                    userId, resource.Id);
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning("Usuario {UserId} intentó acceder al post {PostId} sin ser el propietario",
                    userId, resource.Id);
            }

            return Task.CompletedTask;
        }
    }

    public class CommentModeratorHandlerRequirement : AuthorizationHandler<CommentModeratorRequirement, Comment>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CommentModeratorRequirement requirement,
            Comment resource)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null && resource.Post.AuthorId.ToString() == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}