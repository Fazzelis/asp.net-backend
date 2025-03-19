using Microsoft.AspNetCore.Mvc;
using restApi.Dtos.User;
using restApi.Mappers;
using restApi.Services;

namespace restApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create")]
    public IActionResult CreateUser([FromBody] UserDto userDto)
    {
        var user = _userService.CreateUser(userDto.ToUserModel());
        if (user == null)
        {
            return Conflict("Conflict");
        }
        else
        {
            return Ok(user);
        }
    }

    [HttpGet("login")]
    public IActionResult LoginIn([FromQuery] UserLogin userLogin)
    {
        var userId = _userService.LogIn(userLogin);
        if (userId == null)
        {
            return Conflict("Conflict");
        }
        return Ok(userId);
    }

    [HttpGet("login-with-jwt")]
    public IActionResult LoginInWithJwt([FromHeader] string jwt)
    {
        var user = _userService.LogInWithJwt(jwt);
        if (user == null)
        {
            return BadRequest("Jwt is not correct or token time out");
        }
        return Ok(user.ToUserInfoDto());
    }

    [HttpPatch("give-user-role")]
    public IActionResult giveUserRole([FromHeader] string jwt, [FromBody] UserEmailDto userEmailDto)
    {
        if (_userService.giveUser(jwt, userEmailDto.Email))
        {
            return Ok("Role was add");
        }
        else
        {
            return Conflict("Role was not add");
        }
    }

    [HttpPatch("give-writer-role")]
    public IActionResult giveWritterRole([FromHeader] string jwt, [FromBody] UserEmailDto userEmailDto)
    {
        if (_userService.giveWritter(jwt, userEmailDto.Email))
        {
            return Ok("Role was add");
        }
        else
        {
            return Conflict("Role was not add");
        }
    }

    [HttpPatch("give-admin-role")]
    public IActionResult giveAdminRole([FromHeader] string jwt, [FromBody] UserEmailDto userEmailDto)
    {
        if (_userService.giveAdmin(jwt, userEmailDto.Email))
        {
            return Ok("Role was add");
        }
        else
        {
            return Conflict("Role was not add");
        }
    }
}