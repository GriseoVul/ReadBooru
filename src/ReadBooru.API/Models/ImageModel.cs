using System.Diagnostics.CodeAnalysis;

namespace ReadBooru.API.Models;

public class ImageModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string File { get; set; }
    public byte[] Bytes{ get; set; }
    public int? AuthorId { get; set; } = 1;
    public AccountModel? Author{ get; set; }
}

