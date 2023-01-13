using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StressTestApi_Forum.Models;
using StressTestApi_Forum.Services.Posts;
using StressTestApi_Forum.Services.Users;

namespace StressTestApi_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly IPostService _PostService;
        private readonly IMapper _Mapper;

        public PostsController(IUserService userService, IPostService postService, IMapper mapper)
        {
            _UserService = userService;
            _PostService = postService;
            _Mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IEnumerable<PostDto>> GetPosts([FromQuery] PostFilterSettings settings)
        {
            var posts = await _PostService.GetPostsAsync(settings);

            return _Mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }
}
