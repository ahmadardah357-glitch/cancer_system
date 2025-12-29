using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.Models.Entities
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
        [Column("stage")]
        public string Stage { get; set; }

        [Required]
        [Column("tumor_grade")]
        public string TumorGrade { get; set; }

        [Required]
        [Column("tumor_location")]
        public string TumorLocation { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        // FK → Patient
        [Required]
        [Column("patient_id")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
