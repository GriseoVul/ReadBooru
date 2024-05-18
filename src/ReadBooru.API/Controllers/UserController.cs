using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ReadBooru.API.DAL;
using ReadBooru.API.Models;

namespace ReadBooru.API.Controllers;

[Route("account")]
[ApiController]
[EnableCors]
public class UserController(IUserService UserService, IJwtService JwtService) : ControllerBase
{
    private readonly IUserService userService = UserService;
    private readonly IJwtService jwtService = JwtService;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string Name,string Password)
    {
        var storedUser = await userService.GetUser(Name);
        if (storedUser == default)
            return NotFound( new {Error = "User Not found"});
        if (!userService.IsAuthenticated(Password, storedUser?.PasswordHash))
            return Unauthorized(new {Error = "Wrong password"});

        var tokenString = jwtService.GenerateToken(storedUser);
        return Ok(new { token = tokenString });
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(string Name, string Password)
    {
        var isRegistered = await userService.NewUser(Name, Password);
        if (!isRegistered)
            return Conflict();
        
        return Ok(new { UserRegistered = isRegistered });
    }
}
