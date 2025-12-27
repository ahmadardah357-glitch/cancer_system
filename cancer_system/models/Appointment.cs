using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.models
{
    public class Appointment
    {
        [Key]
        [Column("appointment_id")]
        public int AppointmentId { get; set; }

        [Required]
        [Column("appointment_date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(50)]
        [Column("appointment_type")]
        public string AppointmentType { get; set; }

        [Required]
        [StringLength(50)]
        [Column("status")]
        public string Status { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("recommendations")]
        public string? Recommendations { get; set; }

        // FK → Patient.patient_id
        [Required]
        [Column("patient_id")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        // FK → Doctor.doctor_id
        [Required]
        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
    }
}
