using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
{
    public class Chemotherapy
    {
        [Key]
        public int ChemotherapyId { get; set; }

        [Required]
        public string Drug_Name { get; set; }

        [Required]
        public string Dose { get; set; }

        [Required]
        public DateTime Date { get; set; }

       
        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }

}
