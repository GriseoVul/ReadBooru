using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadBooru.API.DAL;
using ReadBooru.API.Models;

namespace ReadBooru.API.Controllers;

[Route("account")]
[ApiController]
public class UserController(IUserService UserService, IJwtService JwtService) : ControllerBase
{
    private readonly IUserService userService = UserService;
    private readonly IJwtService jwtService = JwtService;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string Name, string Password)
    {
        var storedUser = await userService.GetUser(Name);
        if (!userService.IsAuthenticated(Password, storedUser?.PasswordHash))
            return Unauthorized();

        var tokenString = jwtService.GenerateToken(storedUser);
        return Ok(new { token = tokenString });
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<int> Register(string Name, string PasswordHash)
    {
        return await userService.NewUser(Name, PasswordHash);
    }
}
