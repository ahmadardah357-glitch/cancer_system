namespace cancer_system.DTOs.TestDTO
{
        public class CreateCancerInformationDto
        {
            public string Stage { get; set; }
            public string Tumor_Grade { get; set; }
            public string Diagnosis_Date { get; set; }
            public string Tumor_Location { get; set; }
        }

        public class UpdateCancerInformationDto
        {
            public string Stage { get; set; }
            public string Tumor_Grade { get; set; }
            public string Diagnosis_Date { get; set; }
            public string Tumor_Location { get; set; }
        }
    }

