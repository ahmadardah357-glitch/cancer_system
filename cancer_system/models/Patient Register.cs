using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class Patient_Register
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
