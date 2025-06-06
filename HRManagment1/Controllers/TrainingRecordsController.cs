using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRManagment1.Data;
using HRManagment1.Models;

namespace HRManagment1.Controllers
{
    public class TrainingRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TrainingRecords.Include(t => t.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TrainingRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecords = await _context.TrainingRecords
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TrainingId == id);
            if (trainingRecords == null)
            {
                return NotFound();
            }

            return View(trainingRecords);
        }

        // GET: TrainingRecords/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: TrainingRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingId,TrainingName,Institution,CompletionDate,EmployeeId")] TrainingRecords trainingRecords)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingRecords);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", trainingRecords.EmployeeId);
            return View(trainingRecords);
        }

        // GET: TrainingRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecords = await _context.TrainingRecords.FindAsync(id);
            if (trainingRecords == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", trainingRecords.EmployeeId);
            return View(trainingRecords);
        }

        // POST: TrainingRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingId,TrainingName,Institution,CompletionDate,EmployeeId")] TrainingRecords trainingRecords)
        {
            if (id != trainingRecords.TrainingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingRecords);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingRecordsExists(trainingRecords.TrainingId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", trainingRecords.EmployeeId);
            return View(trainingRecords);
        }

        // GET: TrainingRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecords = await _context.TrainingRecords
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TrainingId == id);
            if (trainingRecords == null)
            {
                return NotFound();
            }

            return View(trainingRecords);
        }

        // POST: TrainingRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingRecords = await _context.TrainingRecords.FindAsync(id);
            if (trainingRecords != null)
            {
                _context.TrainingRecords.Remove(trainingRecords);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingRecordsExists(int id)
        {
            return _context.TrainingRecords.Any(e => e.TrainingId == id);
        }
    }
}
