using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;

namespace NewHorizons.Models
{
    public class Comment
    {
        // Primary Key
        [Key]
        [Display(Name = "Comment ID")]
        public int comment_id { get; set; }

        // Placeholder for UserID FK information
        // Authentication needs to be added first

        // Blog Post Foreign Key
        public int post_id { get; set; }

        // Self-Referencing Foreign Key
        public int? parent_id { get; set; }

        // Navigation Properties
        public Blog_Post Post { get; set; } = null!;
        public Comment? comment { get; set; } = null!;

        // Self-Referencing Navigation
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();

        [Required]
        [Display(Name = "Date Created")]
        public DateTime created_at { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Contents")]
        public string content { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Hidden")]
        public bool is_hidden { get; set; } = false;
    }
}
