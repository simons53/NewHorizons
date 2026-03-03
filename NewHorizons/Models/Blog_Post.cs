using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;

namespace NewHorizons.Models
{
    public class Blog_Post
    {
        [Key]
        [Display(Name = "Post ID")]
        public int post_id { get; set; }

        // Placeholder for UserID FK information
        // Authentication needs to be added first


        // Post Published Status
        [Required]
        [Display(Name = "Published")]
        public bool is_public { get; set; } = false;

        // Post Title
        [Required]
        [Display(Name = "Title")]
        [StringLength(50)]
        public string title { get; set; } = string.Empty;

        // Post Body
        [Required]
        [Display(Name = "Body")]
        public string body { get; set; } = string.Empty;

        // Post Created Date
        [Required]
        [Display(Name = "Created Date")]
        public DateTime created_at { get; set; }

        // Enable/Disable Comments
        [Required]
        public bool comments_enabled { get; set; } = true;

        // Post Author
        [Required]
        public int UserNameId { get; set; }
        public User_Name UserName { get; set; } = null!;

        // Navigation property with initialization
        public ICollection<Comment> comments { get; set; } = new List<Comment>();

    }
}
