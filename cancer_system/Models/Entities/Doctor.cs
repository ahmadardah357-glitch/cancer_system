using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.Models.Entities
{
    public class Doctor
    {
        [Key]
        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(150)]
        [Column("full_name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        [Column("specialization")]
        public string Specialization { get; set; }

        [Required]
        [Column("employee_number")]
        public int EmployeeNumber { get; set; }

        [Column("years_of_experience")]
        public int YearsOfExperience { get; set; }


        // FK → AspNetUsers.Id
        [Required]
        [StringLength(450)]
        [Column("aspnet_user_id")]
        public string AspNetUserId { get; set; }
    }
}
