using AutoMapper;
using Blog.Application.Features.Posts.Commands.CreatePost;
using Blog.Application.Features.Posts.Queries.GetPostsList;
using Blog.Application.Features.Users.Commands.CreateUser;
using Blog.Domain;

namespace Blog.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostVm>();
            CreateMap<CreatePostCommand, Post>();
            CreateMap<CreateUserCommand, User>();
        }
    }
}
