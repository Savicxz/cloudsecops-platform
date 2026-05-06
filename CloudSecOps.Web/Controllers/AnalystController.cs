using System.Security.Claims;
using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Analyst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "SecurityAnalyst")]
public class AnalystController : Controller
{
    private readonly IIncidentService _incidentService;
    private readonly IAssetService _assetService;
    private readonly IVulnerabilityService _vulnerabilityService;

    public AnalystController(
        IIncidentService incidentService,
        IAssetService assetService,
        IVulnerabilityService vulnerabilityService)
    {
        _incidentService = incidentService;
        _assetService = assetService;
        _vulnerabilityService = vulnerabilityService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var model = new AnalystDashboardViewModel
        {
            OpenIncidentCount = await _incidentService.GetOpenCountAsync(),
            HighSeverityIncidentCount = await _incidentService.GetHighSeverityOpenCountAsync(),
            AssetCount = await _assetService.GetActiveCountAsync(),
            HighSeverityVulnerabilityCount = await _vulnerabilityService.GetHighSeverityOpenCountAsync(),
            AssignedOpenIncidents = await _incidentService.GetAssignedOpenAsync(userId, 10),
            HighSeverityIncidents = await _incidentService.GetHighSeverityOpenAsync(10)
        };

        return View(model);
    }

    public async Task<IActionResult> IncidentDetails(Guid id)
    {
        var incident = await _incidentService.GetDetailsAsync(id);
        if (incident == null)
        {
            return NotFound();
        }

        return View(new AnalystIncidentReviewViewModel
        {
            Incident = incident,
            NewStatus = incident.Status,
            AvailableStatuses = Enum.GetValues<IncidentStatus>()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateIncidentStatus(Guid id, IncidentStatus newStatus, string? investigationNote)
    {
        var updated = await _incidentService.UpdateStatusAsync(
            id,
            newStatus,
            investigationNote,
            User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (!updated)
        {
            return NotFound();
        }

        TempData["StatusMessage"] = "Incident status updated.";
        return RedirectToAction(nameof(IncidentDetails), new { id });
    }
}
