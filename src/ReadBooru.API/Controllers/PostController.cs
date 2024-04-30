using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadBooru.API.DAL;
using ReadBooru.API.Models;


namespace ReadBooru.API.Controllers;

[ApiController]
[Route("post")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> logger;
    private readonly IPostRepo postRepo;

    public PostController(ILogger<PostController> logger, IPostRepo repo)
    {
        this.logger = logger;
        this.postRepo = repo;
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<PostModel>> Get()
    {
        
        return await postRepo.GetAllAsync();
    }

    [HttpGet("{id}", Name = "findone")]
    [AllowAnonymous]
    public async Task<ActionResult<PostModel>> Get(int id)
    {
        var result = await postRepo.GetAsync(id);
        if (result != default)
            return Ok(result);
        else
            return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<PostModel>> Insert(PostModel entity)
    {
        var id = await postRepo.AddAsync(entity);
        if(id != default)
            return CreatedAtRoute("findone", new { id }, entity);
        else
            return BadRequest();
    }
    [HttpPut]
    public async Task<ActionResult<PostModel>> Update(PostModel entity)
    {
        var result = await postRepo.UpdateAsync(entity);
        if(result > 0 )
            return NoContent();
        else
            return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await postRepo.DeleteAsync(id);
        if(result > 0)
            return NoContent();
        else
            return Ok();
    }

}
