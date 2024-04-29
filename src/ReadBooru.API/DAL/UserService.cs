using ReadBooru.API.Models;
using BCrypt.Net;
namespace ReadBooru.API.DAL;

public class UserService : IUserService
{
    private readonly List<AccountModel> _accounts;
    public UserService()
    {   
        //TODO link this with DB!
        _accounts =
        [
            new(0, "User1", BCrypt.Net.BCrypt.HashPassword("Password1"), "User"),
            new(1, "Admin1", BCrypt.Net.BCrypt.HashPassword("AdminPass1"), "Admin")
        ];

    }
    public AccountModel? GetUser(string? username)
    {
        ArgumentNullException.ThrowIfNull(username);

        return _accounts.SingleOrDefault(u => u.Name == username);
    }
    public bool IsAuthenticated(string? password, string? passwordHash)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(password);
        ArgumentNullException.ThrowIfNullOrEmpty(passwordHash);

        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
