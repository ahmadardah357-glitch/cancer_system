using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class CBC
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Hemoglobin { get; set; }
        [Required]
        [StringLength(100)]
        public string platelets { get; set; }
        
        [Required]
        [StringLength(100)]
        public string WBC { get; set; }
    }
}
