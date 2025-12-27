using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.models
{
    public class Diagnosis
    {
        [Key]
        [Column("diagnosis_id")]
        public int DiagnosisId { get; set; }

        [Required]
        [Column("diagnosis_date")]
        public DateTime DiagnosisDate { get; set; }

        [Required]
        [Column("notes")]
        public string Notes { get; set; }

        // FK → Patient.patient_id
        [Required]
        [Column("patient_id")]
        public int PatientId { get; set; }

        // Navigation
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
