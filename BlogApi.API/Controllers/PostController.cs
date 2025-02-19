using Blog.Application.Features.Posts.Commands.CreatePost;
using Blog.Application.Features.Posts.Commands.DeletePost;
using Blog.Application.Features.Posts.Commands.UpdatePost;
using Blog.Application.Features.Posts.Queries.GetPostsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "PostOwnerPolicy")]
    public class PostController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost(Name = "CreatePost")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreatePost([FromBody] CreatePostCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet(Name = "GetAllPosts")]
        [ProducesResponseType(typeof(IEnumerable<PostVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PostVm>>> GetAllPosts()
        {
            var query = new GetAllPostsQuery();
            var posts = await _mediator.Send(query);

            return Ok(posts);
        }

        [HttpPut(Name = "UpdatePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdatePost([FromBody] UpdatePostCommand command)
        {
            var updatedPost = await _mediator.Send(command);
            return Ok(updatedPost);
        }

        [HttpDelete("{id}", Name = "DeletePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeletePost(int id)
        {

            var command = new DeletePostCommand
            {
                Id = id
            };

            var deletedPost = await _mediator.Send(command);

            return Ok(deletedPost);
        }

    }
}
