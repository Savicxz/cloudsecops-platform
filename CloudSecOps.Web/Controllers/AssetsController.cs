using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Manager,SecurityAnalyst")]
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

    public async Task<IActionResult> Details(Guid id)
    {
        var asset = await _assetService.GetDetailsAsync(id);
        if (asset == null)
        {
            return NotFound();
        }

        return View(asset);
    }

    public IActionResult Create() => View(new AssetFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AssetFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _assetService.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var model = await _assetService.GetFormAsync(id);
        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, AssetFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _assetService.UpdateAsync(id, model);
        return RedirectToAction(nameof(Details), new { id });
    }
}
