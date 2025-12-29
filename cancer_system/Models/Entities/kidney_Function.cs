using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
{
    public class kidney_Function
    {
        [Key]
        public int KidneyFunctionId { get; set; }

        public string Creatinine { get; set; }
        public string BUN { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
