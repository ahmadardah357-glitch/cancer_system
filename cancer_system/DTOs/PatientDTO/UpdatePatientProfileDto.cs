namespace cancer_system.DTOs.PatientDTO
{
    public class UpdatePatientProfileDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }
        public string CancerType { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
