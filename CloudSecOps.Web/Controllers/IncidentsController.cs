using CloudSecOps.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Reporter,SecurityAnalyst,Manager,Administrator,Auditor")]
public class IncidentsController : Controller
{
    private readonly IIncidentService _incidentService;

    public IncidentsController(IIncidentService incidentService)
    {
        _incidentService = incidentService;
    }

    public async Task<IActionResult> Index()
    {
        // TODO: Restrict reporters to their own incidents and auditors to read-only views.
        var incidents = await _incidentService.GetRecentAsync(25);
        return View(incidents);
    }

    public IActionResult Details(Guid id) => View();

    public IActionResult Create() => View();

    public IActionResult Edit(Guid id) => View();
}
