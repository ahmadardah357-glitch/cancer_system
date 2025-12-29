using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.Models.Entities
{
    public class DoctorPatient
    {
        [Key]
        public int Id { get; set; }

        // FK → Doctor
        [Required]
        [Column("doctor_id")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        // FK → Patient
        [Required]
        [Column("patient_id")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
