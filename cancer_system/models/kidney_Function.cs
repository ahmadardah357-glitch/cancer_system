using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class kidney_Function
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Creatinine { get; set; }
        [Required]
        [StringLength(100)]
        public string BUN { get; set; }
    }
}
