using Microsoft.EntityFrameworkCore;
using ReadBooru.API.Models;

namespace ReadBooru.API.DAL;

public class ImageRepo (AppDBContext dBContext): IImageRepo
{
    private readonly AppDBContext _dbContext = dBContext;
    public async Task<IEnumerable<ImageModel>> GetAllAsync()
    {
        return await _dbContext.ImageModels.ToListAsync();
    }
    public async Task<ImageModel?> GetAsync(int id)
    {
        return await _dbContext.ImageModels.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<int> AddAsync(ImageModel entity)
    {
        _dbContext.Add(entity);
        return await _dbContext.SaveChangesAsync();
    }
    public async Task<int> UpdateAsync(ImageModel entity)
    {
        try 
        {
            _dbContext.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return -1;
        }
    }
    public async Task<int> DeleteAsync(int id)
    {
        try 
        {
            _dbContext.Remove( new ImageModel { Id = id } );
            return await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return -2;
        }
    }
}
