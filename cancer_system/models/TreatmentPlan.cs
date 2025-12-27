using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.models
{
    public class TreatmentPlan
    {
        [Key]
        [Column("plan_id")]
        public int PlanId { get; set; }

        [Required]
        [Column("treatment_type")]
        public string TreatmentType { get; set; }

        [Required]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("duration")]
        public int Duration { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; }

        // FK → Diagnosis.diagnosis_id
        [Required]
        [Column("diagnosis_id")]
        public int DiagnosisId { get; set; }

        // Navigation
        [ForeignKey("DiagnosisId")]
        public Diagnosis Diagnosis { get; set; }
    }
}
