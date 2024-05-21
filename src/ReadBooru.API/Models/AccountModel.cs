using System.ComponentModel.DataAnnotations;

namespace ReadBooru.API.Models;

public class AccountModel 
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public string Role { get; set; } = "User";
    public ICollection<ImageModel>? Images{ get; set; } = [];
    public ICollection<PostModel>? Posts{ get; set; } = [];
}
