namespace cancer_system.DTOs.TestDTO
{
        public class CreateCbcDto
        {
            public string Hemoglobin { get; set; }
            public string Platelets { get; set; }
            public string WBC { get; set; }
        }

        public class UpdateCbcDto
        {
            public string Hemoglobin { get; set; }
            public string Platelets { get; set; }
            public string WBC { get; set; }
        }

        public class CbcDto
        {

        public int CbcId { get; set; }
        public string Hemoglobin { get; set; }
        public string Platelets { get; set; }
        public string WBC { get; set; }

        }


}

