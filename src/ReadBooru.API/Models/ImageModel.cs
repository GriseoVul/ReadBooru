namespace ReadBooru.API.Models;

public class ImageModel
{
    public int Id { get; set; }
    public string File { get; set; } = "../NoImage.png";
    public byte[] Bytes{ get; set; }
}
