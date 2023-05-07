using Microsoft.EntityFrameworkCore;

namespace vjezba5.Models
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options) : base(options) 
        {

        }

        public DbSet<PatientModel> Patients { get; set; }

    }
}
