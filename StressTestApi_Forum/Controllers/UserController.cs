using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StressTestApi_Forum.Models;
using StressTestApi_Forum.Services.Efcore.Entities;
using StressTestApi_Forum.Services.Users;

namespace StressTestApi_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _UserService;
        private readonly IMapper _Mapper;

        public UserController(IUserService userService,IMapper mapper)
        {
            _UserService = userService;
            _Mapper = mapper;
        }

        [HttpGet(Name = "GetUserById")]
        [HttpHead]
        [ProducesDefaultResponseType(typeof(UserDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserAsync(string UserId)
        {
            Guid id = Guid.Empty;

            Guid.TryParse(UserId, out id);

            if (id == Guid.Empty)
            {
                return BadRequest("Id is not a guid");
            }

            var user = await _UserService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            
            var mappedUser = _Mapper.Map<UserDto>(user);

            return Ok(mappedUser);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(statusCode:201,type:typeof(UserDto))]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateUserAsync(UserToBeCreatedDto userToBeCreated)
        {
            try
            {
                var user = _Mapper.Map<User>(userToBeCreated);

                await _UserService.AddUserAsync(user);

                return CreatedAtRoute("GetUserById",new { UserId = user.UserId.ToString() },user);
            }
            catch (Exception e)
            {
                return StatusCode(422, e.Message);
            }
        }

       
    }
}
