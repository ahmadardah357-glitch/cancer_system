using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class Hormonal
    {
        [Key]
        [StringLength(100)]
        public string Drug { get; set; }
        [Required]
        [StringLength(100)]
        public string Dosage { get; set; }
    }
}
