using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class Radiation_Therapy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Sessions_Count { get; set; }
        [Required]
        [StringLength(100)]
        public string Targeted_Area { get; set; }

    }
}
