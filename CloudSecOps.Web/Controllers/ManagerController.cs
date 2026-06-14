using CloudSecOps.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Manager")]
public class ManagerController : Controller
{
    private readonly AppDbContext _context;

    public ManagerController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var reports = await _context.IncidentReports
            .Where(r => r.Status != "Draft")
            .OrderByDescending(r => r.SubmittedAt ?? r.CreatedAt)
            .ToListAsync();

        ViewBag.TotalReports = reports.Count;
        ViewBag.SubmittedReports = reports.Count(r => r.Status == "Submitted");
        ViewBag.UnderReviewReports = reports.Count(r => r.Status == "Under Review");
        ViewBag.AssignedReports = reports.Count(r => r.Status == "Assigned");
        ViewBag.ResolvedReports = reports.Count(r => r.Status == "Resolved");
        ViewBag.HighRiskReports = reports.Count(r => r.Severity == "High" || r.Severity == "Critical");

        return View(reports);
    }

    public async Task<IActionResult> Details(int id)
    {
        var report = await _context.IncidentReports
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id && r.Status != "Draft");

        if (report == null)
        {
            return NotFound();
        }

        return View("~/Views/IncidentReports/Details.cshtml", report);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkUnderReview(int id)
    {
        var report = await _context.IncidentReports.FindAsync(id);

        if (report == null)
        {
            return NotFound();
        }

        if (report.Status != "Submitted")
        {
            return BadRequest();
        }

        report.Status = "Under Review";

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> AssignAnalyst(int id)
    {
        var report = await _context.IncidentReports.FindAsync(id);

        if (report == null)
        {
            return NotFound();
        }

        if (report.Status is not ("Submitted" or "Under Review" or "Assigned"))
        {
            return BadRequest();
        }

        return View(report);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignAnalyst(int id, string assignedAnalyst, string? managerNote)
    {
        var report = await _context.IncidentReports.FindAsync(id);

        if (report == null)
        {
            return NotFound();
        }

        if (report.Status is not ("Submitted" or "Under Review" or "Assigned"))
        {
            return BadRequest();
        }

        if (string.IsNullOrWhiteSpace(assignedAnalyst))
        {
            ModelState.AddModelError("AssignedAnalyst", "Please select or enter an analyst name.");
            return View(report);
        }

        report.AssignedAnalyst = assignedAnalyst.Trim();
        report.ManagerNote = managerNote?.Trim();
        report.AssignedAt = DateTime.UtcNow;
        report.Status = "Assigned";

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> IncidentProgress()
    {
        var reports = await _context.IncidentReports
            .Where(r => r.Status == "Under Review" || r.Status == "Assigned" || r.Status == "Resolved")
            .OrderByDescending(r => r.AssignedAt ?? r.SubmittedAt ?? r.CreatedAt)
            .ToListAsync();

        return View(reports);
    }
}
