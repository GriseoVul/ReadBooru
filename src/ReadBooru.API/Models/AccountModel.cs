using System.ComponentModel.DataAnnotations;

namespace ReadBooru.API.Models;

public class AccountModel(int id, string name, string passwordHash, string role)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string PasswordHash { get; set; } = passwordHash;
    public string Role { get; set; } = role;
}
