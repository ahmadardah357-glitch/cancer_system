namespace cancer_system.DTOs.RadiationTherapyDTO
{
    public class CreateRadiationTherapyDto
    {
        public string Sessions_Count { get; set; }
        public string Targeted_Area { get; set; }
    }

    public class UpdateRadiationTherapyDto
    {
        public string Sessions_Count { get; set; }
        public string Targeted_Area { get; set; }
    }
    public class RadiationTherapyDto
    {
        public int RadiationTherapyId { get; set; }
        public string Sessions_Count { get; set; }
        public string Targeted_Area { get; set; }
    }
}
