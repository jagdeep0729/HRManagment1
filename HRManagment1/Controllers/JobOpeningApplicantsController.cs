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
    public class JobOpeningApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobOpeningApplicantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> MyApplications()
        {
            var username = User.Identity.Name;
            var getApplicantID = _context.Applicant.Where(a => a.Email == username).FirstOrDefault().ApplicantId;
            var getJobOpenings = _context.JobOpeningApplicant.Where(q => q.ApplicantId == getApplicantID).ToList();
            return View(getJobOpenings);
        }

        // GET: JobOpeningApplicants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobOpeningApplicant.Include(j => j.Applicant).Include(j => j.JobOpening);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobOpeningApplicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOpeningApplicant = await _context.JobOpeningApplicant
                .Include(j => j.Applicant)
                .Include(j => j.JobOpening)
                .FirstOrDefaultAsync(m => m.JobOpeningApplicantId == id);
            if (jobOpeningApplicant == null)
            {
                return NotFound();
            }

            return View(jobOpeningApplicant);
        }

        // GET: JobOpeningApplicants/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantId");
            ViewData["JobOpeningId"] = new SelectList(_context.JobOpening, "JobOpeningId", "JobOpeningId");
            return View();
        }

        // POST: JobOpeningApplicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobOpeningApplicantId,ApplicantId,JobOpeningId")] JobOpeningApplicant jobOpeningApplicant)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(jobOpeningApplicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantId", jobOpeningApplicant.ApplicantId);
            ViewData["JobOpeningId"] = new SelectList(_context.JobOpening, "JobOpeningId", "JobOpeningId", jobOpeningApplicant.JobOpeningId);
            return View(jobOpeningApplicant);
        }

        // GET: JobOpeningApplicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOpeningApplicant = await _context.JobOpeningApplicant.FindAsync(id);
            if (jobOpeningApplicant == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantId", jobOpeningApplicant.ApplicantId);
            ViewData["JobOpeningId"] = new SelectList(_context.JobOpening, "JobOpeningId", "JobOpeningId", jobOpeningApplicant.JobOpeningId);
            return View(jobOpeningApplicant);
        }

        // POST: JobOpeningApplicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobOpeningApplicantId,ApplicantId,JobOpeningId")] JobOpeningApplicant jobOpeningApplicant)
        {
            if (id != jobOpeningApplicant.JobOpeningApplicantId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobOpeningApplicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOpeningApplicantExists(jobOpeningApplicant.JobOpeningApplicantId))
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
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantId", jobOpeningApplicant.ApplicantId);
            ViewData["JobOpeningId"] = new SelectList(_context.JobOpening, "JobOpeningId", "JobOpeningId", jobOpeningApplicant.JobOpeningId);
            return View(jobOpeningApplicant);
        }

        // GET: JobOpeningApplicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOpeningApplicant = await _context.JobOpeningApplicant
                .Include(j => j.Applicant)
                .Include(j => j.JobOpening)
                .FirstOrDefaultAsync(m => m.JobOpeningApplicantId == id);
            if (jobOpeningApplicant == null)
            {
                return NotFound();
            }

            return View(jobOpeningApplicant);
        }

        // POST: JobOpeningApplicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobOpeningApplicant = await _context.JobOpeningApplicant.FindAsync(id);
            if (jobOpeningApplicant != null)
            {
                _context.JobOpeningApplicant.Remove(jobOpeningApplicant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobOpeningApplicantExists(int id)
        {
            return _context.JobOpeningApplicant.Any(e => e.JobOpeningApplicantId == id);
        }
    }
}
