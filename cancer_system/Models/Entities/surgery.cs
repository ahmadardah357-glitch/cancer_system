using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
{
    public class surgery
    {
        [Key]
        public int SurgeryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Operation_Type { get; set; }

        [Required]
        public string Surgery_Report { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }

}
