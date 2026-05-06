using System.Security.Claims;
using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

public class AccountController : Controller
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!Enum.TryParse<UserRoles>(model.Role, out var selectedRole))
        {
            ModelState.AddModelError(nameof(model.Role), "Choose a valid role.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var role = selectedRole.ToString();
        var displayName = role == nameof(UserRoles.Administrator) ? "CloudSecOps Administrator" : $"CloudSecOps {role}";

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, model.Email),
            new(ClaimTypes.Name, displayName),
            new(ClaimTypes.Email, model.Email),
            new(ClaimTypes.Role, role)
        };

        var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);

        if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return LocalRedirect(model.ReturnUrl);
        }

        return RedirectToAction("Index", "Dashboard");
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return RedirectToAction("Index", "Home");
    }
}
