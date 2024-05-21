namespace ReadBooru.API.Models;

public class ChapterModel
{
    public int Id { get; set;}
    public string Title { get; set;} = "";
    public string Description { get; set;} = "";
    public ICollection<ImageModel> Images{ get; set;} = [];
}
