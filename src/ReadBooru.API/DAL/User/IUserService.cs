using ReadBooru.API.Models;

namespace ReadBooru.API.DAL;

public interface IUserService
{
    Task<AccountModel?> GetUser(string? username);
    bool IsAuthenticated(string? password, string? PasswordHash);
    Task<bool> NewUser(string Username, string PasswordHash);
}
