using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
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
    //Падла не работает
    public async Task<IActionResult> GetAsync(int id)
    {
        var imageModel = await _dbContext.ImageModels.FirstOrDefaultAsync(x => x.Id == id);
        
        if (imageModel == default)
        {
            imageModel = new ImageModel(){
                Id = 1, 
                File = "E:\\Files\\Images\\NoImage.png", 
                bytes = File.ReadAllBytes("E:\\Files\\Images\\NoImage.png")
            };
        }
        //var image = File.ReadAllBytes(imageModel.File);

        var dataStream = new MemoryStream(imageModel.bytes);
        //Exception System.InvalidOperationException: Timeouts are not supported on this stream.
        // may be it doesn't support json serealise
        return new FileStreamResult(dataStream, "image/png" ){FileDownloadName="file.png"};
    }
    public async Task<IActionResult> GetNoImage()
    {
        var image = File.ReadAllBytesAsync(@"E:\Files\code\Csharp\ReadBooru\NoImage.png");

        var dataStream = new MemoryStream(await image);
        //var base64image = Convert.ToBase64String(image);
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
