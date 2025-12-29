namespace cancer_system.DTOs.TestDTO
{
        public class CreateChemotherapyDto
        {
            public string Drug_Name { get; set; }
            public string Dose { get; set; }
            public DateTime Date { get; set; }
        }

        public class UpdateChemotherapyDto
        {
            public string Drug_Name { get; set; }
            public string Dose { get; set; }
            public DateTime Date { get; set; }
        }
    public class ChemotherapyDto
    {
        public int ChemotherapyId { get; set; }
        public string Drug_Name { get; set; }
        public string Dose { get; set; }
        public DateTime Date { get; set; }
    }
}

