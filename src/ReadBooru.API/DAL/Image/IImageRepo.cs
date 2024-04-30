using ReadBooru.API.Models;
namespace ReadBooru.API.DAL;

public interface IImageRepo
{
    Task<IEnumerable<ImageModel>> GetAllAsync();
    Task<ImageModel?> GetAsync(int id);
    Task<int> AddAsync(ImageModel entity);
    Task<int> UpdateAsync(ImageModel entity);
    Task<int> DeleteAsync(int id);
}
