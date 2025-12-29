using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
{
    public class Cancer_information
    {
        [Key]
        public int Id { get; set; }

        public string Stage { get; set; }
        public string Tumor_Grade { get; set; }
        public string Diagnosis_Date { get; set; }
        public string Tumor_Location { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
