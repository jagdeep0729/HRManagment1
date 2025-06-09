using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRManagment1.Data;
using HRManagment1.Models;
using Microsoft.AspNetCore.Authorization;

namespace HRManagment1.Controllers
{
    public class JobOpeningsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobOpeningsController(ApplicationDbContext context)
        {
            _context = context;
        }
   
        // get: jobopenings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobOpening.Include(j => j.JobPositions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobOpenings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOpening = await _context.JobOpening
                .Include(j => j.JobPositions)
                .FirstOrDefaultAsync(m => m.JobOpeningId == id);
            if (jobOpening == null)
            {
                return NotFound();
            }

            return View(jobOpening);
        }

        // GET: JobOpenings/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Set<JobPositions>(), "PositionId", "PositionId");
            return View();
        }

        // POST: JobOpenings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobOpeningId,PositionId,OpenDate,CloseDate,Status")] JobOpening jobOpening)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobOpening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Set<JobPositions>(), "PositionId", "PositionId", jobOpening.PositionId);
            return View(jobOpening);
        }

        // GET: JobOpenings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOpening = await _context.JobOpening.FindAsync(id);
            if (jobOpening == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Set<JobPositions>(), "PositionId", "PositionId", jobOpening.PositionId);
            return View(jobOpening);
        }

        // POST: JobOpenings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobOpeningId,PositionId,OpenDate,CloseDate,Status")] JobOpening jobOpening)
        {
            if (id != jobOpening.JobOpeningId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobOpening);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOpeningExists(jobOpening.JobOpeningId))
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
            ViewData["PositionId"] = new SelectList(_context.Set<JobPositions>(), "PositionId", "PositionId", jobOpening.PositionId);
            return View(jobOpening);
        }

        // GET: JobOpenings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOpening = await _context.JobOpening
                .Include(j => j.JobPositions)
                .FirstOrDefaultAsync(m => m.JobOpeningId == id);
            if (jobOpening == null)
            {
                return NotFound();
            }

            return View(jobOpening);
        }

        // POST: JobOpenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobOpening = await _context.JobOpening.FindAsync(id);
            if (jobOpening != null)
            {
                _context.JobOpening.Remove(jobOpening);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobOpeningExists(int id)
        {
            return _context.JobOpening.Any(e => e.JobOpeningId == id);
        }
    }
}
