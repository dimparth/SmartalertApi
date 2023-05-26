using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartalert.Api.Datacontracts;
using Smartalert.Api.Interfaces;

namespace Smartalert.Api.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<JWTResponse>> Authenticate([FromForm] AuthenticationRequest user)
        {
            return Ok(await _authenticationService.Authenticate(user));
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserRole([FromForm] string? username)
        {
            return Ok(await _userService.UpdateUserRole(username));
        }
        [HttpGet]
        public async Task<ActionResult<UsersListResponse>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] UserRequest user)
        {
            return Ok(await _userService.AddUser(user));
        }
    }
}
