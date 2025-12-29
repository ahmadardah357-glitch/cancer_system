using cancer_system.DTOs.DiagnosisDTO;
using cancer_system.DTOs.RadiationTherapyDTO;
using cancer_system.DTOs.RadiologyImageDTO;
using cancer_system.DTOs.TestDTO;
using cancer_system.DTOs.TreatmentPlanDTO;

namespace cancer_system.DTOs.MedicalRecordDTO
{
    public class MedicalRecordDto
    {
        public PatientInfoDto Patient { get; set; }

        public List<DiagnosisDto> Diagnoses { get; set; }
        public List<CbcDto> CBC { get; set; }
        public List<ChemotherapyDto> Chemotherapy { get; set; }
        public List<KidneyFunctionDto> KidneyFunction { get; set; }
        public List<LiverFunctionDto> LiverFunction { get; set; }
        public List<RadiationTherapyDto> RadiationTherapy { get; set; }
        public List<SurgeryDto> Surgery { get; set; }
        public List<TreatmentPlanDto> TreatmentPlans { get; set; }
        public List<RadiologyImageDto> RadiologyImages { get; set; }
    }

    public class PatientInfoDto
    {
        public int PatientId { get; set; }

        public string? Name { get; set; }   // من AspNetUsers (nullable)

        public int Age { get; set; }

        public string Gender { get; set; }
        public string CancerType { get; set; }
    }

}
