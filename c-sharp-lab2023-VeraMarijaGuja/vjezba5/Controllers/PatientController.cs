using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vjezba5.Models;

namespace vjezba5.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientContext _context;

        public PatientController(PatientContext context)
        {
            _context = context;
        }

        // GET: Patient
        public async Task<IActionResult> Index()
        {
              return _context.Patients != null ? 
                          View(await _context.Patients.ToListAsync()) :
                          Problem("Entity set 'PatientContext.Patients'  is null.");
        }

        // GET: Patient/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patientModel = await _context.Patients
                .FirstOrDefaultAsync(m => m.Oib == id);
            if (patientModel == null)
            {
                return NotFound();
            }

            return View(patientModel);
        }

        // GET: Patient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Oib,Mbo,FirstName,LastName,Dob,Gender,MedicalDiagnosis,Insurance,Status")] PatientModel patientModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientModel);
        }

        // GET: Patient/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patientModel = await _context.Patients.FindAsync(id);
            if (patientModel == null)
            {
                return NotFound();
            }
            return View(patientModel);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var patientToUpdate = await _context.Patients.FirstOrDefaultAsync(p => p.Oib == id);
            if (await TryUpdateModelAsync<PatientModel>(
                patientToUpdate,
                "",
                p => p.MedicalDiagnosis, p => p.Insurance, p => p.Status))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(patientToUpdate);
        }

        // GET: Patient/ShowActive
        public async Task<IActionResult> ShowActive()
        {
            return _context.Patients != null ?
                         View(await _context.Patients
                         .Where(p => p.Status == "active")
                         .ToListAsync()) :
                         Problem("Entity set 'PatientContext.Patients'  is null.");
        }

        // GET: Patient/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patientModel = await _context.Patients
                .FirstOrDefaultAsync(m => m.Oib == id);
            if (patientModel == null)
            {
                return NotFound();
            }

            return View(patientModel);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            /*if (_context.Patients == null)
            {
                return Problem("Entity set 'PatientContext.Patients'  is null.");
            }
            var patientModel = await _context.Patients.FindAsync(id);
            if (patientModel != null)
            {
                _context.Patients.Remove(patientModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));*/
            if (_context.Patients == null)
            {
                return Problem("Entity set 'PatientContext.Patients'  is null.");
            }
            var patientModel = await _context.Patients.FindAsync(id);
            if (patientModel != null)
            {
                patientModel.Status = "discharged";
                _context.SaveChanges();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }

        private bool PatientModelExists(string id)
        {
          return (_context.Patients?.Any(e => e.Oib == id)).GetValueOrDefault();
        }
    }
}
