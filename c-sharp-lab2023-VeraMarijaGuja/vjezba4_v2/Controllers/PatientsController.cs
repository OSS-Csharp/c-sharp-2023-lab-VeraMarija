using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vjezba4_v2.Models;

namespace vjezba4_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientContext _context;

        public PatientsController(PatientContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            return await _context.Patients.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(string id)
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(string id, Patient patient)
        {
            if (id != patient.Oib)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
          if (_context.Patients == null)
          {
              return Problem("Entity set 'PatientContext.Patients'  is null.");
          }
            _context.Patients.Add(patient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(patient.Oib))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatient", new { id = patient.Oib }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(string id)
        {
            return (_context.Patients?.Any(e => e.Oib == id)).GetValueOrDefault();
        }
    }
}
