using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Incidents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CloudSecOps.Web.Controllers
{
    public class IncidentReportsController : Controller
    {
        private readonly AppDbContext _context;

        public IncidentReportsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reports = await _context.IncidentReports
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(reports);
        }

        public async Task<IActionResult> Details(int id)
        {
            var report = await _context.IncidentReports
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentReport report)
        {
            if (!ModelState.IsValid)
            {
                return View(report);
            }

            report.Status = "Draft";
            report.ReporterName = GetCurrentReporterName();
            report.CreatedAt = DateTime.UtcNow;
            report.SubmittedAt = null;

            _context.IncidentReports.Add(report);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Report draft saved successfully. You can review and submit it when ready.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var report = await _context.IncidentReports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            if (report.Status != "Draft")
            {
                TempData["ErrorMessage"] = "Only draft reports can be edited.";
                return RedirectToAction(nameof(Details), new { id = report.Id });
            }

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IncidentReport updatedReport)
        {
            if (id != updatedReport.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedReport);
            }

            var report = await _context.IncidentReports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            if (report.Status != "Draft")
            {
                TempData["ErrorMessage"] = "Only draft reports can be edited.";
                return RedirectToAction(nameof(Details), new { id = report.Id });
            }

            report.Title = updatedReport.Title;
            report.Category = updatedReport.Category;
            report.Severity = updatedReport.Severity;
            report.AffectedAsset = updatedReport.AffectedAsset;
            report.Description = updatedReport.Description;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Draft report updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int id)
        {
            var report = await _context.IncidentReports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            if (report.Status != "Draft")
            {
                TempData["ErrorMessage"] = "Only draft reports can be submitted.";
                return RedirectToAction(nameof(Details), new { id = report.Id });
            }

            report.Status = "Submitted";
            report.SubmittedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Suspicious activity report submitted successfully.";

            return RedirectToAction(nameof(Index));
        }

        private string GetCurrentReporterName()
        {
            return User.FindFirstValue(ClaimTypes.Name)
                ?? User.FindFirstValue(ClaimTypes.Email)
                ?? User.Identity?.Name
                ?? "Reporter User";
        }
    }
}