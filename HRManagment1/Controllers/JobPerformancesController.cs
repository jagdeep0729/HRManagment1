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
    public class JobPerformancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPerformancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobPerformances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobPerformance.Include(j => j.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobPerformances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPerformance = await _context.JobPerformance
                .Include(j => j.Employee)
                .FirstOrDefaultAsync(m => m.PerformanceId == id);
            if (jobPerformance == null)
            {
                return NotFound();
            }

            return View(jobPerformance);
        }

        // GET: JobPerformances/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: JobPerformances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerformanceId,ReviewDate,Score,Reviewer,Comments,EmployeeId")] JobPerformance jobPerformance)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(jobPerformance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", jobPerformance.EmployeeId);
            return View(jobPerformance);
        }

        // GET: JobPerformances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPerformance = await _context.JobPerformance.FindAsync(id);
            if (jobPerformance == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", jobPerformance.EmployeeId);
            return View(jobPerformance);
        }

        // POST: JobPerformances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerformanceId,ReviewDate,Score,Reviewer,Comments,EmployeeId")] JobPerformance jobPerformance)
        {
            if (id != jobPerformance.PerformanceId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPerformance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPerformanceExists(jobPerformance.PerformanceId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", jobPerformance.EmployeeId);
            return View(jobPerformance);
        }

        // GET: JobPerformances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPerformance = await _context.JobPerformance
                .Include(j => j.Employee)
                .FirstOrDefaultAsync(m => m.PerformanceId == id);
            if (jobPerformance == null)
            {
                return NotFound();
            }

            return View(jobPerformance);
        }

        // POST: JobPerformances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPerformance = await _context.JobPerformance.FindAsync(id);
            if (jobPerformance != null)
            {
                _context.JobPerformance.Remove(jobPerformance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPerformanceExists(int id)
        {
            return _context.JobPerformance.Any(e => e.PerformanceId == id);
        }
    }
}
