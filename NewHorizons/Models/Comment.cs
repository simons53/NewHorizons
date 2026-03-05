using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHorizons.Models
{
    public class Comment
    {
        // Primary Key
        [Key]
        [Display(Name = "Comment ID")]
        public int CommentId { get; set; }

        // Post Body
        [Required]
        [StringLength(5000)]
        [Display(Name = "Body")]
        public string Body { get; set; } = string.Empty;

        // Created Date/Time
        [Required]
        [Display(Name = "Date Created")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Comment Visibility
        [Required]
        [Display(Name = "Hidden")]
        public bool IsHidden { get; set; } = false;

        // Comment belongs to a User
        [Required]
        public string AuthorId { get; set; } = string.Empty;
        public ApplicationUser Author { get; set; } = null!;

        // Comment belongs to a Post
        public int PostId { get; set; }
        public BlogPost Post { get; set; } = null!;

        // Self-Referencing Foreign Key (Replies)
        public int? ParentId { get; set; }
        public Comment? ParentComment { get; set; }

        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}
