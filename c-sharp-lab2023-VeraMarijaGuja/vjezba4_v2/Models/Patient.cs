﻿using System.ComponentModel.DataAnnotations;

namespace vjezba4_v2.Models
{
    public class Patient
    {
        [Key]
        public string Oib { get; set; }
        public string Mbo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        public string Gender { get; set; }
        public string MedicalDiagnosis { get; set; }
        public string Insurance { get; set; }
        public string Status { get; set; }
    }


}
