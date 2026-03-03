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

        // Post Body
        [Required]
        [StringLength(1000)]
        [Display(Name = "Body")]
        public string Body { get; set; } = string.Empty;

        // Created Date/Time
        [Required]
        [Display(Name = "Date Created")]
        public DateTime created_at { get; set; }

        // Comment Visibility
        [Required]
        [Display(Name = "Hidden")]
        public bool is_hidden { get; set; } = false;

        // Comment belongs to a User
        [Required]
        public int UserNameId { get; set; }
        public User_Name UserName { get; set; } = null!;

        // Comment belongs to a Post
        [ForeignKey("post_id")]
        public int post_id { get; set; }
        public Blog_Post Post { get; set; } = null!;

        // Self-Referencing Foreign Key (Replies)
        public int? parent_id { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public Comment? ParentComment { get; set; } = null!;
    }
}
