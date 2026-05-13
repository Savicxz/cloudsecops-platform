using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Incidents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "SecurityAnalyst")]
public class IncidentsController : Controller
{
    private readonly IIncidentService _incidentService;

    public IncidentsController(IIncidentService incidentService)
    {
        _incidentService = incidentService;
    }

    public async Task<IActionResult> Index()
    {
        var incidents = await _incidentService.GetRecentAsync(25);
        return View(incidents);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var incident = await _incidentService.GetDetailsAsync(id);
        if (incident == null)
        {
            return NotFound();
        }

        return View(incident);
    }

    public IActionResult Create() => View(new IncidentFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IncidentFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _incidentService.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var model = await _incidentService.GetFormAsync(id);
        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, IncidentFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _incidentService.UpdateAsync(id, model);
        return RedirectToAction(nameof(Details), new { id });
    }
}
