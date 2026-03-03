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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User 1 -> N Blog Posts
            modelBuilder.Entity<Blog_Post>()
                .HasOne(p =>p.UserName)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserNameId)
                .OnDelete(DeleteBehavior.Restrict);

            // User 1 => N Comments
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.UserName)
                .WithMany(u => u.Comments)
                .HasForeignKey(c  => c.UserNameId)
                .OnDelete(DeleteBehavior.Restrict);

            // Blog Post 1 -> Comments
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.comments)
                .HasForeignKey(c => c.post_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment 1-> Comment (Replies) (Self-Referencing)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(p => p.Replies)
                .HasForeignKey(c => c.parent_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.post_id);

            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.parent_id);

            modelBuilder.Entity<Blog_Post>()
                .HasIndex(p => p.UserNameId);
        }
    }


}
