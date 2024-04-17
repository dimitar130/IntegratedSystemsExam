using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Domain.DTO;

namespace IntegratedSystems.Web.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index(Guid? id)
        {
            
            return View(await _context.Patients.ToListAsync());
                    
        }

        public async Task<IActionResult> PatientsForCenter(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["centerId"] = id;
            ICollection<Vaccine> vaccines =  await _context.Vaccines.Where(v => v.VaccinationCenter.Equals(id)).ToListAsync();
            ICollection<Patient> model = new HashSet<Patient>();
            foreach(var vaccine in vaccines)
            {
                model.Add(await _context.Patients.Where(p => p.Id.Equals(vaccine.PatientId)).FirstOrDefaultAsync());
            }
            return View(model);
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Embg,FirstName,LastName,PhoneNumber,Email,Id")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Id = Guid.NewGuid();
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Embg,FirstName,LastName,PhoneNumber,Email,Id")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(Guid id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }

        public IActionResult AddPatientToCenter(Guid id)
        {
            AddPatientToCenterDTO model = new AddPatientToCenterDTO()
            {
                CenterId = (Guid)id,
                Manufacturer = "",
                DateTaken = DateTime.Now,
                PatientId = new Guid()
            };

            ViewData["Items"] = new SelectList(_context.Patients, "Id", "FirstName");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatientToCenter([Bind("CenterId", "Manufacturer", "DataTaken", "PatientId")] AddPatientToCenterDTO item)
        {

            VaccinationCenter? center = await _context.VaccinationCenters.Where(c => c.Id.Equals(item.CenterId)).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {

                if (center.MaxCapacity == 0)
                {
                    ViewData["Name"] = center.Name;
                    return RedirectToAction("ErrorPage");
                };

                Vaccine model = new Vaccine()
                {
                    Manufacturer = item.Manufacturer,
                    Certificate = Guid.NewGuid(),
                    DateTaken = item.DateTaken,
                    PatientId = item.PatientId,
                    PatientFor = await _context.Patients.Where(p => p.Id.Equals(item.PatientId)).FirstOrDefaultAsync(),
                    VaccinationCenter = item.CenterId,
                    Center = center
                };

                model.Center.MaxCapacity--;
                _context.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("PatientsForCenter","Patients", new { id = item.CenterId});
            }            

            return RedirectToAction("Index","VaccinationCenters");
        }

        public IActionResult ErrorPage(Guid centerId)
        {
            return View();
        }
    }
}
