﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadBooru.API.DAL;
using ReadBooru.API.Models;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection.Metadata.Ecma335;

namespace ReadBooru;

[ApiController]
[Route("image")]
[Authorize]
public class ImageController(ILogger<ImageController> Logger, IImageRepo ImageRepo, IConfiguration configuration) : ControllerBase
{
    private  readonly ILogger<ImageController> logger = Logger;
    private readonly IImageRepo imageRepo = ImageRepo;
    private readonly IConfiguration _config = configuration;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<ImageModel>> Get()
    {
        return await imageRepo.GetAllAsync();
    }

    // [HttpGet("{id}", Name = "imageFindOne")]
    // [AllowAnonymous]
    // public async Task<IActionResult> Get(int id)
    // {
    //     return await imageRepo.GetAsync(id);
    // }

    [HttpGet("{id}", Name = "imageFindSeveral")]
    [AllowAnonymous]
    public async Task<IEnumerable<ImageModel>> GetCountFromIndex(int id, [FromQuery] int count)
    {
        return await imageRepo.GetSeveralFrom(id, count);
    }

    //delete this
    [HttpGet("/test")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNoImage()
    {
        return await imageRepo.GetNoImage();
    }

    //TODO move implementation and do path validation in ImageRepo
    [HttpPost(Name = "saveImage")]
    public async Task<ActionResult<ImageModel>> Post(List<IFormFile> file)
    {
        long size = file.Sum(f => f.Length);
        var id = 0; 
        
        foreach (IFormFile formFile in file)
        {
            if (formFile.Length > 0)
            {   
                var storageFolder = _config["StoredFilePath"];
                var filePath = Path.Combine(storageFolder, Path.GetRandomFileName() + ".png");
                
                using (var FileStream = new FileStream(filePath, FileMode.CreateNew))
                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream);
                    id = await imageRepo.AddAsync(new ImageModel{
                        Id=0, 
                        File=filePath, 
                        Bytes=stream.ToArray()
                    });
                }
            }
        }

        //do path validation

        return id == default ? NotFound() : CreatedAtRoute("saveImage", new {id});
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
