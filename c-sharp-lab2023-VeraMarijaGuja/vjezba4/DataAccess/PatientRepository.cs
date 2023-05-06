using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace vjezba4.DataAccess
{
    internal class PatientRepository
    {
        private PatientEntities patientContext = null;

        public PatientRepository()
        {
            patientContext = new PatientEntities();
        }

        public Patient Get(string oib)
        {
            return patientContext.Patients.Find(oib);
        }

        public List<Patient> GetAll()
        {
            return patientContext.Patients.ToList();
        }

        public List<Patient> GetByStatus(string status)
        {
            List<Patient> patients= new List<Patient>();
            if(status == "active")
            {
                patients = patientContext.Patients
                     .Where(p => p.Status == "active")
                     .ToList(); 
            }
            else
            {
                patients = patientContext.Patients
                     .Where(p => p.Status == "discharged")
                     .ToList();
            }
            return patients;
        }

        public void AddPatient(Patient patient)
        {
            if (patient != null)
            {
                patientContext.Patients.Add(patient);
                patientContext.SaveChanges();
            }
        }

        public void UpdatePatient(Patient patient)
        {
            var patientFind = this.Get(patient.Oib);
            if (patientFind != null)
            {
                patientFind.FirstName = patient.FirstName;
                patientFind.LastName = patient.LastName;
                patientFind.Oib = patient.Oib;
                patientFind.Mbo = patient.Mbo;
                patientFind.Dob = patient.Dob;
                patientFind.Gender = patient.Gender;
                patientFind.MedicalDiagnosis = patient.MedicalDiagnosis;
                patientFind.Status = patient.Status;
                patientFind.Insurance = patient.Insurance;
                patientContext.SaveChanges();
            }
        }

        public void DischargePatient(string oib)
        {
            var patObj = patientContext.Patients.Find(oib);
            if (patObj != null)
            {
                patObj.Status = "discharged";
                patientContext.SaveChanges();
            }
        }
    }
}

