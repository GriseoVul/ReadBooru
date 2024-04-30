namespace ReadBooru.API.DAL;
using ReadBooru.API.Models;
public interface IJwtService
{
    string GenerateToken(AccountModel? user);
}
