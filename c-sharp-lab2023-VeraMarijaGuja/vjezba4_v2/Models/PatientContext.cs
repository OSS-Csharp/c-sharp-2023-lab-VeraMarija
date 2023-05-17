using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace vjezba4_v2.Models
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options) : base(options) 
        { 
        }

        public DbSet<Patient> Patients { get; set; } = null!;
    }
}
