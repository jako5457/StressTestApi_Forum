using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StressTestApi_Forum.Models;
using StressTestApi_Forum.Services.Users;

namespace StressTestApi_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _UserService;
        private readonly IMapper _Mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _UserService = userService;
            _Mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _UserService.GetUsersAsync();
            return _Mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
