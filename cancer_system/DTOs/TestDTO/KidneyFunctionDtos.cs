
namespace cancer_system.DTOs.TestDTO
{
    
        public class CreateKidneyFunctionDto
        {
            public string Creatinine { get; set; }
            public string BUN { get; set; }
        }

        public class UpdateKidneyFunctionDto
        {
            public string Creatinine { get; set; }
            public string BUN { get; set; }
        }
    public class KidneyFunctionDto
    {
        public int KidneyFunctionId { get; set; }
        public string Creatinine { get; set; }
        public string BUN { get; set; }
    }
}

