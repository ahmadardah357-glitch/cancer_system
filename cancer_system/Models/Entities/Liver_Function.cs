using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
{
    public class Liver_Function
    {
        [Key]
        public int LiverFunctionId { get; set; }

        public string ALT { get; set; }
        public string AST { get; set; }
        public string AlP { get; set; }
        public string Bilirubin { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
