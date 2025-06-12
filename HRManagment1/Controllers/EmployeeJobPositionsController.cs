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
    public class EmployeeJobPositionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeJobPositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeJobPositions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeJobPosition.Include(e => e.Employee).Include(e => e.JobPositions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeJobPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeJobPosition = await _context.EmployeeJobPosition
                .Include(e => e.Employee)
                .Include(e => e.JobPositions)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (employeeJobPosition == null)
            {
                return NotFound();
            }

            return View(employeeJobPosition);
        }

        // GET: EmployeeJobPositions/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId");
            ViewData["PositionName"] = new SelectList(_context.Set<JobPositions>(), "PositionName", "PositionName");
            return View();
        }

        // POST: EmployeeJobPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,PositionName,StartDate,Enddate,EmployeeId")] EmployeeJobPosition employeeJobPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeJobPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", employeeJobPosition.EmployeeId);
            ViewData["PositionName"] = new SelectList(_context.Set<JobPositions>(), "PositionId", "PositionId", employeeJobPosition.PositionId);
            return View(employeeJobPosition);
        }

        // GET: EmployeeJobPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeJobPosition = await _context.EmployeeJobPosition.FindAsync(id);
            if (employeeJobPosition == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", employeeJobPosition.EmployeeId);
            ViewData["PositionId"] = new SelectList(_context.Set<JobPositions>(), "PositionId", "PositionId", employeeJobPosition.PositionId);
            return View(employeeJobPosition);
        }

        // POST: EmployeeJobPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,PositionId,StartDate,Enddate,EmployeeId")] EmployeeJobPosition employeeJobPosition)
        {
            if (id != employeeJobPosition.RecordId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeJobPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeJobPositionExists(employeeJobPosition.RecordId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", employeeJobPosition.EmployeeId);
            ViewData["PositionId"] = new SelectList(_context.Set<JobPositions>(), "PositionId", "PositionId", employeeJobPosition.PositionId);
            return View(employeeJobPosition);
        }

        // GET: EmployeeJobPositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeJobPosition = await _context.EmployeeJobPosition
                .Include(e => e.Employee)
                .Include(e => e.JobPositions)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (employeeJobPosition == null)
            {
                return NotFound();
            }

            return View(employeeJobPosition);
        }

        // POST: EmployeeJobPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeJobPosition = await _context.EmployeeJobPosition.FindAsync(id);
            if (employeeJobPosition != null)
            {
                _context.EmployeeJobPosition.Remove(employeeJobPosition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeJobPositionExists(int id)
        {
            return _context.EmployeeJobPosition.Any(e => e.RecordId == id);
        }
    }
}
