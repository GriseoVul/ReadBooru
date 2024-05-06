using Microsoft.AspNetCore.Mvc;
using ReadBooru.API.Models;
namespace ReadBooru.API.DAL;

public interface IImageRepo
{
    Task<IEnumerable<ImageModel>> GetAllAsync();
    Task<IActionResult> GetAsync(int id);
    Task<IActionResult> GetNoImage();
    Task<int> AddAsync(ImageModel entity);
    Task<int> UpdateAsync(ImageModel entity);
    Task<int> DeleteAsync(int id);
}
