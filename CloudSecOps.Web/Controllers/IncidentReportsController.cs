using System.Security.Claims;
using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Incidents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Reporter")]
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
            .AsNoTracking()
            .Where(report => report.ReporterName == GetCurrentReporterName())
            .OrderByDescending(report => report.CreatedAt)
            .ToListAsync();

        return View(reports);
    }

    public async Task<IActionResult> Details(int id)
    {
        var report = await GetReporterReportAsync(id);
        if (report == null)
        {
            return NotFound();
        }

        return View(report);
    }

    public IActionResult Create()
    {
        return View(new IncidentReport());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IncidentReport report, string submitAction)
    {
        if (!ModelState.IsValid)
        {
            return View(report);
        }

        var now = DateTime.UtcNow;
        var isSubmit = string.Equals(submitAction, "Submit", StringComparison.OrdinalIgnoreCase);

        report.Status = isSubmit ? "Submitted" : "Draft";
        report.ReporterName = GetCurrentReporterName();
        report.CreatedAt = now;
        report.SubmittedAt = isSubmit ? now : null;

        _context.IncidentReports.Add(report);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = isSubmit
            ? "Report submitted for analyst review."
            : "Report draft saved.";

        return RedirectToAction(nameof(Details), new { id = report.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var report = await GetReporterReportAsync(id);
        if (report == null)
        {
            return NotFound();
        }

        if (!string.Equals(report.Status, "Draft", StringComparison.OrdinalIgnoreCase))
        {
            TempData["StatusMessage"] = "Only draft reports can be edited.";
            return RedirectToAction(nameof(Details), new { id = report.Id });
        }

        return View(report);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, IncidentReport updatedReport, string submitAction)
    {
        if (id != updatedReport.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(updatedReport);
        }

        var report = await _context.IncidentReports
            .FirstOrDefaultAsync(item => item.Id == id && item.ReporterName == GetCurrentReporterName());

        if (report == null)
        {
            return NotFound();
        }

        if (!string.Equals(report.Status, "Draft", StringComparison.OrdinalIgnoreCase))
        {
            TempData["StatusMessage"] = "Only draft reports can be edited.";
            return RedirectToAction(nameof(Details), new { id = report.Id });
        }

        report.Title = updatedReport.Title;
        report.Category = updatedReport.Category;
        report.Severity = updatedReport.Severity;
        report.AffectedAsset = updatedReport.AffectedAsset;
        report.Description = updatedReport.Description;

        if (string.Equals(submitAction, "Submit", StringComparison.OrdinalIgnoreCase))
        {
            report.Status = "Submitted";
            report.SubmittedAt = DateTime.UtcNow;
            TempData["StatusMessage"] = "Report submitted for analyst review.";
        }
        else
        {
            TempData["StatusMessage"] = "Draft report updated.";
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = report.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(int id)
    {
        var report = await _context.IncidentReports
            .FirstOrDefaultAsync(item => item.Id == id && item.ReporterName == GetCurrentReporterName());

        if (report == null)
        {
            return NotFound();
        }

        if (!string.Equals(report.Status, "Draft", StringComparison.OrdinalIgnoreCase))
        {
            TempData["StatusMessage"] = "Only draft reports can be submitted.";
            return RedirectToAction(nameof(Details), new { id = report.Id });
        }

        report.Status = "Submitted";
        report.SubmittedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Report submitted for analyst review.";
        return RedirectToAction(nameof(Details), new { id = report.Id });
    }

    private async Task<IncidentReport?> GetReporterReportAsync(int id)
    {
        return await _context.IncidentReports
            .AsNoTracking()
            .FirstOrDefaultAsync(report => report.Id == id && report.ReporterName == GetCurrentReporterName());
    }

    private string GetCurrentReporterName()
    {
        return User.FindFirstValue(ClaimTypes.Name)
            ?? User.FindFirstValue(ClaimTypes.Email)
            ?? User.Identity?.Name
            ?? "Reporter User";
    }
}
