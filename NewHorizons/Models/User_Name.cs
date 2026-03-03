using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;

namespace NewHorizons.Models
{
    public class User_Name
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="User Name")]
        [StringLength(25, ErrorMessage = "Please enter a user name using 25 characters or less.")]
        public string UserName { get; set; } = string.Empty;

        // Placeholder for Authentication FK

    }
}
