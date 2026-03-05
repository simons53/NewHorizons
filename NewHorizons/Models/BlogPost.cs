using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHorizons.Models
{
    public class BlogPost
    {
        [Key]
        [Display(Name = "Post ID")]
        public int PostId { get; set; }

        // Post Published Status
        [Required]
        [Display(Name = "Published")]
        public bool IsPublic { get; set; } = false;

        // Post Title
        [Required]
        [Display(Name = "Title")]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        // Post Body
        [Required]
        [Display(Name = "Body")]
        public string BodyHtml { get; set; } = string.Empty;

        // Post Created Date
        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Enable/Disable Comments
        [Required]
        public bool CommentsEnabled { get; set; } = true;

        // Post Author
        [Required]
        public string AuthorId { get; set; } = string.Empty;
        public ApplicationUser Author { get; set; } = null!;

        // Navigation property with initialization
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
