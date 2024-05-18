using System.ComponentModel.DataAnnotations;

namespace ReadBooru.API.Models;

public class AccountModel 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public ICollection<ImageModel> Images{ get; set; } = [];
    public ICollection<PostModel> Posts{ get; set; } = [];
}
