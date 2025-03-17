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
        var user = _userService.CreateUser(UserMapper.ToUserModel(userDto));
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
    public IActionResult LoginInWithJwt([FromHeader] string token)
    {
        var user = _userService.LogInWithJwt(token);
        if (user == null)
        {
            return BadRequest("Jwt is not correct or token time out");
        }
        return Ok(UserMapper.ToUserInfoDto(user));
    }
}