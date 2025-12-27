using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class Chemotherapy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Drug_Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Dose { get; set; }
        [Required]
        [StringLength(100)]
        public string Date { get; set; }
    }
}
