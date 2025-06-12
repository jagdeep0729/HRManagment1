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
    public class LeaveRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveRecord.Include(l => l.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRecord = await _context.LeaveRecord
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leaveRecord == null)
            {
                return NotFound();
            }

            return View(leaveRecord);
        }

        // GET: LeaveRecords/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: LeaveRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveId,LeaveType,Status,StartDate,EndDate,EmployeeId")] LeaveRecord leaveRecord)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(leaveRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", leaveRecord.EmployeeId);
            return View(leaveRecord);
        }

        // GET: LeaveRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRecord = await _context.LeaveRecord.FindAsync(id);
            if (leaveRecord == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", leaveRecord.EmployeeId);
            return View(leaveRecord);
        }

        // POST: LeaveRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveId,LeaveType,Status,StartDate,EndDate,EmployeeId")] LeaveRecord leaveRecord)
        {
            if (id != leaveRecord.LeaveId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRecordExists(leaveRecord.LeaveId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", leaveRecord.EmployeeId);
            return View(leaveRecord);
        }

        // GET: LeaveRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRecord = await _context.LeaveRecord
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leaveRecord == null)
            {
                return NotFound();
            }

            return View(leaveRecord);
        }

        // POST: LeaveRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRecord = await _context.LeaveRecord.FindAsync(id);
            if (leaveRecord != null)
            {
                _context.LeaveRecord.Remove(leaveRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRecordExists(int id)
        {
            return _context.LeaveRecord.Any(e => e.LeaveId == id);
        }
    }
}
