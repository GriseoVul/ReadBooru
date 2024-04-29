using Microsoft.EntityFrameworkCore;
using ReadBooru.API.Models;
namespace ReadBooru.API;

public class AppDBContext : DbContext
{
    public DbSet<PostModel> PostModels{ get; set; }
    
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

}
