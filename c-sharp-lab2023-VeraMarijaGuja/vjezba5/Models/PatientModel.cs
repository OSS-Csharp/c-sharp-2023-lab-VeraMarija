namespace vjezba5.Models
{
    public class PatientModel
    {
        public string Oib { get; set; }
        public string Mbo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? MedicalDiagnosis { get; set; }
        public string? Insurance { get; set; }
        public string? Status { get; set; }
    }
}
