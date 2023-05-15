namespace vjezba5.Models
{
    public static class DbInitializer
    {
        public static void Initialize(PatientContext patientContext)
        {
            patientContext.Database.EnsureCreated();

            // Look for any students.
            if (patientContext.Patients.Any())
            {
                return;   // DB has been seeded
            }

            var patients = new PatientModel[]
            {
            new PatientModel
            {
                Oib="43136070281",
                Mbo="12345678",
                FirstName="Vera Marija",
                LastName= "Guja",
                Dob =  DateTime.Parse("2005-09-01"),
                Gender = "female",
                Insurance = "osnovno",
                MedicalDiagnosis = "dijagnoza233",
                Status = "active"}
            };
            foreach (PatientModel p in patients)
            {
                patientContext.Patients.Add(p);
            }
            patientContext.SaveChanges();



        }
    }
}

