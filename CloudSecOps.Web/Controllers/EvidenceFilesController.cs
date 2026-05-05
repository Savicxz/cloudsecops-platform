using CloudSecOps.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize]
public class EvidenceFilesController : Controller
{
    private readonly IEvidenceFileService _evidenceFileService;

    public EvidenceFilesController(IEvidenceFileService evidenceFileService)
    {
        _evidenceFileService = evidenceFileService;
    }

    public async Task<IActionResult> Index()
    {
        var files = await _evidenceFileService.GetRecentAsync(25);
        return View(files);
    }

    public IActionResult Details(Guid id) => View();
}
