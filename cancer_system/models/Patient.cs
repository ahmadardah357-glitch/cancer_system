using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.models
{
    public class Patient
    {
        [Key]
        [Column("patient_id")]
        public int PatientId { get; set; }

        [Required]
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
        //

        [Required]
        [StringLength(10)]
        [Column("gender")]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        [Column("cancer_type")]

        public string cancer_type { get; set; }

        // FK → AspNetUsers.Id
        [Required]
        [StringLength(450)]
        [Column("UserId")]
        public string AspNetUserId { get; set; }

    }
}
