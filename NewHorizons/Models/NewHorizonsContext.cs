using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NewHorizons.Models;

namespace NewHorizons.Models
{
    public class NewHorizonsContext : IdentityDbContext<ApplicationUser>
    {
        public NewHorizonsContext(DbContextOptions<NewHorizonsContext> options) : base(options) 
        { 

        }
        public DbSet<BlogPost> Posts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User 1 -> N Blog Posts
            modelBuilder.Entity<BlogPost>()
                .HasOne(p =>p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // User 1 => N Comments
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c  => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Blog Post 1 -> Comments
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment 1-> Comment (Replies) (Self-Referencing)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(p => p.Replies)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.PostId);

            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.ParentId);

            modelBuilder.Entity<BlogPost>()
                .HasIndex(p => p.AuthorId);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.DisplayName)
                .IsUnique();
        }
    }


}
