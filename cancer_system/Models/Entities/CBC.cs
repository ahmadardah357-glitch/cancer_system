using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
{
    public class CBC
    {
        [Key]
        public int CbcId { get; set; }

        public string Hemoglobin { get; set; }
        public string Platelets { get; set; }
        public string WBC { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }

}
