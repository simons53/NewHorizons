using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;

namespace NewHorizons.Models
{
    public class User_Name
    {
        public int UserNameId { get; set; }

        // User Display Name
        [Required]
        [Display(Name ="User Name")]
        [StringLength(25, ErrorMessage = "Please enter a user name using 25 characters or less.")]
        public string DisplayName { get; set; } = string.Empty;

        // Placeholder for Authentication FK
        // Need to create Authentication first?

        // Navigation
        public ICollection<Blog_Post> Posts { get; set; } = new List<Blog_Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
