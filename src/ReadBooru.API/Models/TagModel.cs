namespace ReadBooru.API.Models;

public class TagModel
{
    public int Id { get; set; }
    public string Tag { get; set; } = "tagme";
    public ICollection<PostModel> Posts{ get; set; } = [];

}
