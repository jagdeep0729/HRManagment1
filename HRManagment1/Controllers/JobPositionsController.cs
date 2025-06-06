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
    public class JobPositionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobPositions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobPositions.Include(j => j.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPositions = await _context.JobPositions
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.PositionId == id);
            if (jobPositions == null)
            {
                return NotFound();
            }

            return View(jobPositions);
        }

        // GET: JobPositions/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentId");
            return View();
        }

        // POST: JobPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionId,Title,DepartmentId,SalaryGrade")] JobPositions jobPositions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPositions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentId", jobPositions.DepartmentId);
            return View(jobPositions);
        }

        // GET: JobPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPositions = await _context.JobPositions.FindAsync(id);
            if (jobPositions == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentId", jobPositions.DepartmentId);
            return View(jobPositions);
        }

        // POST: JobPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PositionId,Title,DepartmentId,SalaryGrade")] JobPositions jobPositions)
        {
            if (id != jobPositions.PositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPositions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPositionsExists(jobPositions.PositionId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentId", jobPositions.DepartmentId);
            return View(jobPositions);
        }

        // GET: JobPositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPositions = await _context.JobPositions
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.PositionId == id);
            if (jobPositions == null)
            {
                return NotFound();
            }

            return View(jobPositions);
        }

        // POST: JobPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPositions = await _context.JobPositions.FindAsync(id);
            if (jobPositions != null)
            {
                _context.JobPositions.Remove(jobPositions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPositionsExists(int id)
        {
            return _context.JobPositions.Any(e => e.PositionId == id);
        }
    }
}
