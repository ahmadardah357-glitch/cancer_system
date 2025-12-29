using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
{
    public class Radiation_Therapy
    {
        [Key]
        public int RadiationTherapyId { get; set; }

        [Required]
        public string Sessions_Count { get; set; }

        [Required]
        public string Targeted_Area { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }

}
