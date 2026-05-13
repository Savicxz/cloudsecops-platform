using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Incidents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Reporter")]
public class ReporterController : Controller
{
    private readonly AppDbContext _context;

    public ReporterController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult SubmitReport() => View(new IncidentReport());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitReport(IncidentReport report, string submitAction)
    {
        if (!ModelState.IsValid)
        {
            return View(report);
        }

        var now = DateTime.UtcNow;
        var isSubmit = string.Equals(submitAction, "Submit", StringComparison.OrdinalIgnoreCase);

        report.Status = isSubmit ? "Submitted" : "Draft";
        report.ReporterName = GetReporterName();
        report.CreatedAt = now;
        report.SubmittedAt = isSubmit ? now : null;

        _context.IncidentReports.Add(report);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = isSubmit
            ? "Report submitted for analyst review."
            : "Report draft saved.";

        return RedirectToAction(nameof(Details), new { id = report.Id });
    }

    public async Task<IActionResult> MyReports()
    {
        var reporterName = GetReporterName();
        var reports = await _context.IncidentReports
            .AsNoTracking()
            .Where(report => report.ReporterName == reporterName)
            .OrderByDescending(report => report.CreatedAt)
            .ToListAsync();

        return View(reports);
    }

    public async Task<IActionResult> Details(int id)
    {
        var reporterName = GetReporterName();
        var report = await _context.IncidentReports
            .AsNoTracking()
            .FirstOrDefaultAsync(report => report.Id == id && report.ReporterName == reporterName);

        if (report == null)
        {
            return NotFound();
        }

        return View(report);
    }

    private string GetReporterName()
    {
        return User.FindFirstValue(ClaimTypes.Name)
            ?? User.FindFirstValue(ClaimTypes.Email)
            ?? User.Identity?.Name
            ?? "Reporter User";
    }
}
