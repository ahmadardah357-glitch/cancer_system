using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
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
