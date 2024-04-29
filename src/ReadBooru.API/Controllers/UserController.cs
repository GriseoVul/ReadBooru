using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadBooru.API.DAL;
using ReadBooru.API.Models;

namespace ReadBooru.API.Controllers;

[Route("account")]
[ApiController]
public class UserController(IUserService userService, IJwtService jwtService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IJwtService _jwtService = jwtService;

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(string Name, string Password)
    {
        var storedUser = _userService.GetUser(Name);
        if (!_userService.IsAuthenticated(Password, storedUser?.PasswordHash))
            return Unauthorized();

        var tokenString = _jwtService.GenerateToken(storedUser);
        return Ok(new { token = tokenString });
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register(string Name, string Password)
    {
        throw new NotImplementedException();
    }
}
