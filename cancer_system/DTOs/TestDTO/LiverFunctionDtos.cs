namespace cancer_system.DTOs.TestDTO
{
  
        public class CreateLiverFunctionDto
        {
            public string ALT { get; set; }
            public string AST { get; set; }
            public string AlP { get; set; }
            public string Bilirubin { get; set; }
        }

        public class UpdateLiverFunctionDto
        {
            public string ALT { get; set; }
            public string AST { get; set; }
            public string AlP { get; set; }
            public string Bilirubin { get; set; }
        }
    public class LiverFunctionDto
    {
        public int LiverFunctionId { get; set; }
        public string ALT { get; set; }
        public string AST { get; set; }
        public string AlP { get; set; }
        public string Bilirubin { get; set; }
    }
}

