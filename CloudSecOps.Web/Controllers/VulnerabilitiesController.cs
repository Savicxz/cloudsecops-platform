using CloudSecOps.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Analyst,Manager,Admin,Auditor")]
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

    public IActionResult Details(Guid id) => View();

    public IActionResult Create() => View();

    public IActionResult Edit(Guid id) => View();
}
