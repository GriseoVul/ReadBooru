namespace ReadBooru.API.DAL;

using Microsoft.EntityFrameworkCore;
using ReadBooru.API.Models;
public class PostRepo(AppDBContext dBContext): IPostRepo
{
    private readonly AppDBContext _dbContext = dBContext;
    public async Task<IEnumerable<PostModel>> GetAllAsync()
    {
        return await _dbContext.PostModels.ToListAsync();
    }
    public async Task<PostModel?> GetAsync(int id)
    {
        return await _dbContext.PostModels.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<int> AddAsync(PostModel entity)
    {
        _dbContext.Add(entity);
        return await _dbContext.SaveChangesAsync();
    }
    public async Task<int> UpdateAsync(PostModel entity)
    {
        try
        {
            _dbContext.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            return -1;
        }
    }
    public async Task<int> DeleteAsync(int id)
    {
        try
        {
            _dbContext.PostModels.Remove(
                new PostModel{Id = id}
            );
            return await _dbContext.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            return -1;
        }
    }
}
