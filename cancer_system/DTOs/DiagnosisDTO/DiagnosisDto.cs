namespace cancer_system.DTOs.DiagnosisDTO
{
    
    public class CreateDiagnosisDto
    {
        public DateTime DiagnosisDate { get; set; }
        public string Stage { get; set; }
        public string TumorGrade { get; set; }
        public string TumorLocation { get; set; }
        public string? Notes { get; set; }
    }

    
    public class UpdateDiagnosisDto
    {
        public DateTime DiagnosisDate { get; set; }
        public string Stage { get; set; }
        public string TumorGrade { get; set; }
        public string TumorLocation { get; set; }
        public string? Notes { get; set; }
    }

    
    public class DiagnosisDto
    {
        public int DiagnosisId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string Stage { get; set; }
        public string TumorGrade { get; set; }
        public string TumorLocation { get; set; }
        public string? Notes { get; set; }
    }
}
