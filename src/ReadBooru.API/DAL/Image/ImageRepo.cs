using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadBooru.API.Models;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using Microsoft.IdentityModel.Tokens;

namespace ReadBooru.API.DAL;

public class ImageRepo (AppDBContext dBContext): IImageRepo
{
    private readonly AppDBContext _dbContext = dBContext;

    public async Task<IEnumerable<ImageModel>> GetAllAsync()
    {
        return await _dbContext.ImageModels.ToListAsync();
    }
    //Падла работает
    public async Task<IActionResult> GetAsync(int id)
    {
        var imageModel = await _dbContext.ImageModels.FirstOrDefaultAsync(x => x.Id == id);
        
        if (imageModel == default)
        {
            imageModel = new ImageModel(){
                Id = 1, 
                File = "E:\\Files\\Images\\NoImage.png", 
                Bytes = File.ReadAllBytes("E:\\Files\\Images\\NoImage.png")
            };
        }
        return new FileStreamResult(new MemoryStream(imageModel.Bytes), "image/png");
    }

    //implement logic for this
    public async Task<IEnumerable<ImageModel>> GetSeveralFrom(int id, int count = 1)
    {
        List<ImageModel> images = await _dbContext.ImageModels.OrderBy( x => x.Id )
        .Where(x => x.Id >= id)
        .Take(count)
        .ToListAsync();
        return images;
    }

    public async Task<IActionResult> GetNoImage()
    {
        var image = File.ReadAllBytesAsync(@"E:\Files\code\Csharp\ReadBooru\NoImage.png");

        var dataStream = new MemoryStream(await image);
        return new FileStreamResult(dataStream, "image/png");
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
