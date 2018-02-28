using Microsoft.EntityFrameworkCore;
 
namespace belt2.Models
{
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Idea> Ideas {get; set;}
        public DbSet<Like> Likes {get; set;}
    }
}