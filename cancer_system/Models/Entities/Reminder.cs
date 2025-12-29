using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.Models.Entities
{
    public class Reminder
    {
        [Key]
        [Column("reminder_id")]
        public int ReminderId { get; set; }

        [Required]
        [Column("reminder_time")]
        public DateTime ReminderTime { get; set; }

        [Required]
        [StringLength(50)]
        [Column("reminder_type")]
        public string ReminderType { get; set; }

        [Required]
        [StringLength(50)]
        [Column("status")]
        public string Status { get; set; }

        // FK → Appointment.appointment_id
        [Required]
        [Column("appointment_id")]
        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
    }
}
