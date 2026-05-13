using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Vulnerabilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "SecurityAnalyst,Manager,Auditor")]
public class VulnerabilitiesController : Controller
{
    private readonly IVulnerabilityService _vulnerabilityService;

    public VulnerabilitiesController(IVulnerabilityService vulnerabilityService)
    {
        _vulnerabilityService = vulnerabilityService;
    }

    public async Task<IActionResult> Index()
    {
        var vulnerabilities = await _vulnerabilityService.GetRecentAsync(25);
        return View(vulnerabilities);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var vulnerability = await _vulnerabilityService.GetDetailsAsync(id);
        if (vulnerability == null)
        {
            return NotFound();
        }

        return View(vulnerability);
    }

    public IActionResult Create() => View(new VulnerabilityFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VulnerabilityFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _vulnerabilityService.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var model = await _vulnerabilityService.GetFormAsync(id);
        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, VulnerabilityFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _vulnerabilityService.UpdateAsync(id, model);
        return RedirectToAction(nameof(Details), new { id });
    }
}
