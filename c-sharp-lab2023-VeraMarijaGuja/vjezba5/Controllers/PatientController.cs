using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vjezba5.Models;

namespace vjezba5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientContext _dbContext;

        public PatientController(PatientContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientModel>>> GetPatients()
        {
            if (_dbContext.Patients == null)
            {
                return NotFound();
            }
            return await _dbContext.Patients.ToListAsync();
        }

        [HttpGet("{oib}")]
        public async Task<ActionResult<PatientModel>> GetPatient(string oib)
        {
            if (_dbContext.Patients == null)
            {
                return NotFound();
            }
            var patient = await _dbContext.Patients.FindAsync(oib);
            if(patient == null)
            {
                return NotFound();
            }
            return patient;
        }

        [HttpPost]
        public async Task<ActionResult<PatientModel>> PostPatient(PatientModel patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatient), new { Oib = patient.Oib }, patient);
        }

        [HttpPut]
        public async Task<IActionResult> PutPatient(string oib, PatientModel patient)
        {
            if(oib != patient.Oib)
            {
                return BadRequest();
            }
            _dbContext.Entry(patient).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientAvailable(oib))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool PatientAvailable(string oib)
        {
            return (_dbContext.Patients?.Any(x => x.Oib == oib)).GetValueOrDefault();
        }

        [HttpDelete("{oib}")]
        public async Task<IActionResult> DeletePatient(string oib)
        {
            if(_dbContext.Patients == null)
            {
                return NotFound();
            }

            var patient = await _dbContext.Patients.FindAsync(oib);
            if(patient == null)
            {
                return NotFound();
            }

            _dbContext.Patients.Remove(patient);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
