using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class cancer_information
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string stage { get; set; }
        [Required]
        [StringLength(100)]
        public string tumor_grade { get; set; }
        [Required]
        [StringLength(100)]
        public string diagnosis_data { get; set; }
        [Required]
        [StringLength(100)]
        
        public string tumor_location { get; set; }
    }
}
