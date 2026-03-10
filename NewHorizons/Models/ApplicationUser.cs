using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NewHorizons.Models
{
    [Index(nameof(DisplayName), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        // Display name shown publicly on blog posts
        [Required]
        [Display(Name = "User Name")]
        [StringLength(25, ErrorMessage = "Please enter a user name using 25 characters or less.")]
        public string DisplayName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool HasDisplayName { get; set; } = false;

        // Navigation properties
        public ICollection<BlogPost> Posts { get; set; } = new List<BlogPost>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}