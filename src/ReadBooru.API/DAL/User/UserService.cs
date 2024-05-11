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

    public async Task<int> NewUser(string Username, string Password)
    {
        //
        _dbContext.Users.Add(new AccountModel (0,Username, BCrypt.Net.BCrypt.HashPassword(Password),"User") );

        return  await _dbContext.SaveChangesAsync();
    }
}
