using cancer_system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Column("full_name")]
        public string FullName { get; set; }
        public Patient? Patient { get; set; }
    }
}
