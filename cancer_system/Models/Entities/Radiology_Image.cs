using cancer_system.Models.Entities;
using System.ComponentModel.DataAnnotations;

public class Radiology_Image
{
    [Key]
    public int RadiologyImageId { get; set; }

    [Required]
    public string ImagePath { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public int PatientId { get; set; }

    public Patient Patient { get; set; }
}
