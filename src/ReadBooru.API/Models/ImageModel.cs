using System.Diagnostics.CodeAnalysis;

namespace ReadBooru.API.Models;

public class ImageModel()
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = String.Empty;
    public string File { get; set; } = String.Empty;
    public byte[] Bytes{ get; set; } = [];
    
    public AccountModel? Author{ get; set; }
    public int? AuthorId { get; set; }
    
    public ImageModel( AccountModel? author=null): this(){
        this.Author = author;
        this.AuthorId = author?.Id ?? 0;
    }
}

