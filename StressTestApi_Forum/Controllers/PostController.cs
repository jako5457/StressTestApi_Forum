using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StressTestApi_Forum.Models;
using StressTestApi_Forum.Services.Efcore.Entities;
using StressTestApi_Forum.Services.Posts;
using StressTestApi_Forum.Services.Users;
using System.Reflection.Metadata.Ecma335;

namespace StressTestApi_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly IPostService _PostService;
        private readonly IMapper _Mapper;

        public PostController(IUserService userService, IPostService postService, IMapper mapper)
        {
            _UserService = userService;
            _PostService = postService;
            _Mapper = mapper;
        }

        [HttpGet(Name = "ApiGetPost")]
        [HttpHead]
        [ProducesResponseType(statusCode:200,type:typeof(PostDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPost(string PostId)
        {
            Guid id = Guid.Empty;

            Guid.TryParse(PostId, out id);

            if (id == Guid.Empty)
            {
                return BadRequest("Id is not a guid");
            }

            var post = await _PostService.GetPostAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(_Mapper.Map<PostDto>(post));
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(PostDto))]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreatePost(string UserId,PostToBeCreatedDto post)
        {
            try
            {
                var newpost = _Mapper.Map<Post>(post);

                newpost.Auhtorid = Guid.Parse(UserId);
                newpost.CategoryId = 1;

                var postResult = await _PostService.CreatePostAsync(newpost);
                return CreatedAtRoute("ApiGetPost",new { PostId = postResult.PostId },postResult);
            }
            catch (Exception e)
            {
                return StatusCode(422,e.Message);  
            }
        }

        [HttpDelete]
        [Route("remove")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemovePost(string PostId)
        {
            try
            {
                Guid id = Guid.Empty;

                Guid.TryParse(PostId, out id);

                if (id == Guid.Empty)
                {
                    return BadRequest("Id is not a guid");
                }

                await _PostService.RemovePostAsync(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
