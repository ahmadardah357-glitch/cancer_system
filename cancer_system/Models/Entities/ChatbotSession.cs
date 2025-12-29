using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.Models.Entities
{
    public class ChatbotSession
    {
        [Key]
        [Column("session_id")]
        public int SessionId { get; set; }

        // FK → Patient.patient_id
        [Required]
        [Column("patient_id")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [Required]
        [Column("question_text")]
        public string QuestionText { get; set; }

        [Column("reply")]
        public string? Reply { get; set; }

        [Required]
        [Column("session_date")]
        public DateTime SessionDate { get; set; } = DateTime.UtcNow;

        [Column("session_type")]
        public string? SessionType { get; set; }
    }
}
