using Microsoft.EntityFrameworkCore;
using NewHorizons.Models;

namespace NewHorizons.Models
{
    public class NewHorizonsContext : DbContext
    {
        public NewHorizonsContext(DbContextOptions<NewHorizonsContext> options) : base(options) 
        { 

        }
        public DbSet<Blog_Post> Posts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<User_Name> UserNames { get; set; } = null!;
    }
}
