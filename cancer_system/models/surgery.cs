using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class surgery
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(100)]
        public string Operation_Type { get; set; }
        [Required]
        [StringLength(100)]
        public string surgery_Report { get; set; }

    }
}
