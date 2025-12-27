using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.models
{
    public class Attends
    {

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }


        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

    }
}
