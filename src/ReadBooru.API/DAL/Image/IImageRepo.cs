using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using ReadBooru.API.Models;
namespace ReadBooru.API.DAL;

public interface IImageRepo
{
    Task<IEnumerable<ImageModel>> GetAllAsync();
    Task<IActionResult> GetAsync(int id);
    Task<IEnumerable<ImageModel>> GetSeveralFrom(int id, int count);
    Task<IActionResult> GetNoImage();
    Task<int> AddAsync(ImageModel entity);
    Task<int> AddSeveral(List<IFormFile> files, IIdentity user);
    Task<int> UpdateAsync(ImageModel entity);
    Task<int> DeleteAsync(int id);
}
