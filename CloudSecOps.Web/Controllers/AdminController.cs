using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.Models.Identity;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IIncidentService _incidentService;
    private readonly IIncidentReportService _incidentReportService;
    private readonly IAssetService _assetService;
    private readonly IVulnerabilityService _vulnerabilityService;
    private readonly IAuditLogService _auditLogService;

    public AdminController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IIncidentService incidentService,
        IIncidentReportService incidentReportService,
        IAssetService assetService,
        IVulnerabilityService vulnerabilityService,
        IAuditLogService auditLogService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _incidentService = incidentService;
        _incidentReportService = incidentReportService;
        _assetService = assetService;
        _vulnerabilityService = vulnerabilityService;
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            UserCount = await _userManager.Users.CountAsync(),
            RoleCount = await _roleManager.Roles.CountAsync(),
            IncidentCount = await _incidentService.GetTotalCountAsync(),
            IncidentReportCount = await _incidentReportService.GetTotalCountAsync(),
            AssetCount = await _assetService.GetTotalCountAsync(),
            VulnerabilityCount = await _vulnerabilityService.GetTotalCountAsync(),
            AuditLogCount = await _auditLogService.GetTotalCountAsync(),
            RecentAuditLogs = await _auditLogService.GetRecentAsync(5)
        };

        return View(model);
    }

    public async Task<IActionResult> Users()
    {
        var users = await _userManager.Users
            .OrderBy(user => user.Email)
            .ToListAsync();

        var userItems = new List<UserRoleListItemViewModel>();
        foreach (var user in users)
        {
            userItems.Add(new UserRoleListItemViewModel
            {
                UserId = user.Id,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Department = user.Department,
                JobTitle = user.JobTitle,
                Roles = (await _userManager.GetRolesAsync(user)).OrderBy(role => role).ToList()
            });
        }

        var model = new UserRoleOverviewViewModel
        {
            Users = userItems,
            Roles = await _roleManager.Roles
                .OrderBy(role => role.Name)
                .Select(role => role.Name ?? string.Empty)
                .ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateUserRoles(string userId, List<string> selectedRoles)
    {
        selectedRoles ??= [];

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var validRoles = await _roleManager.Roles
            .Select(role => role.Name ?? string.Empty)
            .Where(role => role != string.Empty)
            .ToListAsync();

        selectedRoles = selectedRoles
            .Where(role => validRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var currentUserIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if ((currentUserIdentifier == userId || currentUserIdentifier == user.Email)
            && !selectedRoles.Contains(nameof(UserRoles.Administrator), StringComparer.OrdinalIgnoreCase))
        {
            TempData["StatusMessage"] = "You cannot remove your own Administrator role.";
            return RedirectToAction(nameof(Users));
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = selectedRoles.Except(currentRoles, StringComparer.OrdinalIgnoreCase).ToList();
        var rolesToRemove = currentRoles.Except(selectedRoles, StringComparer.OrdinalIgnoreCase).ToList();

        if (rolesToAdd.Count > 0)
        {
            await _userManager.AddToRolesAsync(user, rolesToAdd);
        }

        if (rolesToRemove.Count > 0)
        {
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
        }

        await _auditLogService.RecordAsync(
            "User roles updated",
            nameof(ApplicationUser),
            user.Id,
            $"Roles: {string.Join(", ", selectedRoles.OrderBy(role => role))}",
            User.FindFirstValue(ClaimTypes.NameIdentifier));

        TempData["StatusMessage"] = $"Roles updated for {user.Email}.";
        return RedirectToAction(nameof(Users));
    }

    public async Task<IActionResult> SystemStatus()
    {
        var expectedRoles = Enum.GetNames<UserRoles>().OrderBy(role => role).ToList();
        var existingRoles = await _roleManager.Roles
            .Select(role => role.Name ?? string.Empty)
            .ToListAsync();
        var missingRoles = expectedRoles
            .Except(existingRoles, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var model = new SystemSeedStatusViewModel
        {
            ExpectedRoles = expectedRoles,
            MissingRoles = missingRoles,
            RolesSeeded = missingRoles.Count == 0,
            DemoUserCount = await _userManager.Users.CountAsync(user =>
                user.Email != null && user.Email.EndsWith("@cloudsecops.local")),
            DemoUsersSeeded = await _userManager.Users.CountAsync(user =>
                user.Email != null && user.Email.EndsWith("@cloudsecops.local")) >= expectedRoles.Count,
            UserCount = await _userManager.Users.CountAsync(),
            RoleCount = await _roleManager.Roles.CountAsync(),
            IncidentCount = await _incidentService.GetTotalCountAsync(),
            IncidentReportCount = await _incidentReportService.GetTotalCountAsync(),
            AuditLogCount = await _auditLogService.GetTotalCountAsync()
        };

        return View(model);
    }
}
