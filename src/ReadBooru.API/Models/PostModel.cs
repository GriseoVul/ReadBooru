namespace ReadBooru.API.Models;


public class PostModelShort
{
    public int Id { get; set;}
    public string Title { get; set;} = "";
}

public class PostModel : PostModelShort
{
    public string Description { get; set;} = "";
    public ICollection<ChapterModel> Chapters{ get; set;} = [];
    public ICollection<TagModel> Tags{ get; set;} = [];
}

