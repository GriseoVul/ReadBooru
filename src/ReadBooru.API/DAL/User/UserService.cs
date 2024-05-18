using ReadBooru.API.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
namespace ReadBooru.API.DAL;

public class UserService(AppDBContext dBContext) : IUserService
{
    private readonly AppDBContext _dbContext = dBContext;

    public Task<AccountModel?> GetUser(string? username)
    {
        ArgumentNullException.ThrowIfNull(username);

        return _dbContext.Users.FirstOrDefaultAsync(x => x.Name == username);
        // return _accounts.SingleOrDefault(u => u.Name == username);
    }
    public bool IsAuthenticated(string? password, string? passwordHash)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(password);
        ArgumentNullException.ThrowIfNullOrEmpty(passwordHash);

        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public async Task<bool> NewUser(string Username, string Password)
    {
        //
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == Username);
        
        if (user != default)
            return false;
        var salt = BCrypt.Net.BCrypt.GenerateSalt(10);

        _dbContext.Users.Add(new AccountModel()
        {
            Id=0,
            Name=Username, 
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password),
            Role = "User"
        });
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
