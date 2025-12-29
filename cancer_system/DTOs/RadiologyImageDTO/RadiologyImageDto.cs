namespace cancer_system.DTOs.RadiologyImageDTO
{
    public class CreateRadiologyImageDto
    {
        public IFormFile Image { get; set; }
    }
    public class RadiologyImageDto
    {
        public int RadiologyImageId { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
