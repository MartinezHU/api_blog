
using AutoMapper;
using Blog.Application.Contracts.Persistence;
using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetPostsList
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, List<PostVm>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<List<PostVm>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<List<PostVm>>(posts);
        }
    }

}
