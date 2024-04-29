namespace ReadBooru.API.Models;

public class PostModel
{
    public int Id { get; set;}
    public string Title { get; set;} = "";
    public string Description { get; set;} = "";
    public int? Media_Id { get; set;}
    public int? Tags_id{ get; set;}
}
