namespace cancer_system.Models.Entities
{
    public class Attends
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
