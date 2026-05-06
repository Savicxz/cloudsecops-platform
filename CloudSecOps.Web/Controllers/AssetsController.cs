using CloudSecOps.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Manager,Administrator,SecurityAnalyst")]
public class AssetsController : Controller
{
    private readonly IAssetService _assetService;

    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    public async Task<IActionResult> Index()
    {
        var assets = await _assetService.GetRecentAsync(25);
        return View(assets);
    }

    public IActionResult Details(Guid id) => View();

    public IActionResult Create() => View();

    public IActionResult Edit(Guid id) => View();
}
