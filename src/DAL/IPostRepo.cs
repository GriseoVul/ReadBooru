using ReadBooru.API.Models;
namespace ReadBooru.API.DAL;

public interface IPostRepo
{
    
    Task<IEnumerable<PostModel>> GetAllAsync();
    Task<PostModel?> GetAsync(int id);
    Task<int> AddAsync(PostModel entity);
    Task<int> UpdateAsync(PostModel entity);
    Task<int> DeleteAsync(int id);
}
