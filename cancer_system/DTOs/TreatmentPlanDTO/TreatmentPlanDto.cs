namespace cancer_system.DTOs.TreatmentPlanDTO
{
    public class CreateTreatmentPlanDto
    {
        public string TreatmentType { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public int DiagnosisId { get; set; }
    }

    public class UpdateTreatmentPlanDto
    {
        public string TreatmentType { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
    }
    public class TreatmentPlanDto
    {
        public int TreatmentPlanId { get; set; }
        public string TreatmentType { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public int DiagnosisId { get; set; }
    }
}
