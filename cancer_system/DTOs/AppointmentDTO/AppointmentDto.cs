namespace cancer_system.DTOs.AppointmentDTO
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentType { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
        public string? Recommendations { get; set; }
    }

    public class CreateAppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public string AppointmentType { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateAppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
        public string? Recommendations { get; set; }
    }
}
