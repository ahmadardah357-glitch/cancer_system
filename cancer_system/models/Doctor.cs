using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.models
{
    public class Doctor
    {
        [Key]
        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("specialization")]
        public string Specialization { get; set; }

        [Required]
        [StringLength(100)]
        [Column("doctorID")]
        public int doctorID { get; set; }
        
        // FK → AspNetUsers.Id
        [Required]
        [StringLength(450)]
        [Column("aspnet_user_id")]
        public string AspNetUserId { get; set; }
    }
}
