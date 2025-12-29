namespace cancer_system.DTOs.DoctorDTO
{
    public class UpdateDoctorProfileDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string Specialization { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
