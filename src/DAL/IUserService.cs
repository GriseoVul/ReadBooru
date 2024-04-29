using ReadBooru.API.Models;

namespace ReadBooru.API.DAL;

public interface IUserService
{
    AccountModel? GetUser(string? username);
    bool IsAuthenticated(string? password, string? PasswordHash);
}
