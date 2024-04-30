using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadBooru.API.DAL;
using ReadBooru.API.Models;

namespace ReadBooru;

[ApiController]
[Route("image")]
[Authorize]
public class ImageController(ILogger<ImageController> Logger, IImageRepo ImageRepo) : ControllerBase
{
    private  readonly ILogger<ImageController> logger = Logger;
    private readonly IImageRepo imageRepo = ImageRepo;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<ImageModel>> Get()
    {
        return await imageRepo.GetAllAsync();
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ImageModel>> Get(int id)
    {
        var result = await imageRepo.GetAsync(id);


        return result == default ? NotFound() : Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<ImageModel>> Post(ImageModel image)
    {
        var id = await imageRepo.AddAsync(image);
        return id == default ? NotFound() : CreatedAtRoute("findone", new {id}, image);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<ImageModel>> Delete(int id)
    {
        var result = await imageRepo.DeleteAsync(id);
        if (result == default) 
            return NotFound();
        return Ok();
    }
}
