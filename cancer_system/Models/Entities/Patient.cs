using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cancer_system.Models.Identity;

namespace cancer_system.Models.Entities
{
    public class Patient
    {
        [Key]
        [Column("patient_id")]
        public int PatientId { get; set; }

        [Required]
        [StringLength(150)]
        [Column("full_name")]
        public string FullName { get; set; }

        [Required]
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        [Column("gender")]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        [Column("cancer_type")]
        public string CancerType { get; set; }

      
        [Required]
        [StringLength(450)]
        [Column("UserId")]
        public string AspNetUserId { get; set; }

        
        public ApplicationUser User { get; set; }

     
        public ICollection<Radiology_Image> RadiologyImages { get; set; }
    }
}
