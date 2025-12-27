using System.ComponentModel.DataAnnotations;

namespace cancer_system.models
{
    public class Liver_Function
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ALT { get; set; }
        [Required]
        [StringLength(100)]
        public string AST { get; set; }
        [Required]
        [StringLength(100)]
        public string AlP { get; set; }
        [Required]
        [StringLength(100)]
        public string Billrubin { get; set; }
    }
}
