using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Incidents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Controllers
{
  [Authorize(Roles = "Reporter")]
  public class IncidentReportsController : Controller{
    private readonly AppDbContext _context;

    public IncidentReportsController(AppDbContext context){
      _context = context;
    }

    public async Task<IActionResult> Index(){
      var reports = await _context.IncidentReports
        .OrderByDescending(r => r.CreatedAt)
        .ToListAsync();

        return View(reports);
    }

    public async Task<IActionResult> Details(int id){
      var report = await _context.IncidentReports
        .FirstOrDefaultAsync(r => r.Id == id);

      if (report == null){
        return NotFound();
      }

      return View(report);
    }

    public IActionResult Create(){
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IncidentReport report){
      if(!ModelState.IsValid){
        return View(report);
      }

      report.Status = "Reported";
      report.ReporterName = "Reported User";
      report.CreatedAt = DateTime.UtcNow;

      _context.IncidentReports.Add(report);
      await _context.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }
  }
}
