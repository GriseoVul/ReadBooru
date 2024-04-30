using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using ReadBooru.API.Models;
namespace ReadBooru.API;

public class AppDBContext : DbContext
{
    public DbSet<PostModel> PostModels{ get; set; }
    public DbSet<ImageModel> ImageModels{ get; set; }
    public DbSet<TagModel> TagModels{ get; set; }

    public DbSet<AccountModel> Users { get; set; }
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        try{
            var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
            databaseCreator.CreateTables();
        }
        catch( MySqlException )
        {
            
        }
        Database.EnsureCreated();
    }

}
