using CloudSecOps.Web.Services.Interfaces;
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

    public IActionResult Create() => View();

    public IActionResult Edit(Guid id) => View();
}
