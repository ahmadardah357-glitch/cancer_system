namespace cancer_system.DTOs.TestDTO
{
        public class CreateSurgeryDto
        {
            public string Operation_Type { get; set; }
            public string Surgery_Report { get; set; }
        }

        public class UpdateSurgeryDto
        {
            public string Operation_Type { get; set; }
            public string Surgery_Report { get; set; }
        }
    public class SurgeryDto
    {
        public int SurgeryId { get; set; }
        public string Operation_Type { get; set; }
        public string Surgery_Report { get; set; }
    }
}

