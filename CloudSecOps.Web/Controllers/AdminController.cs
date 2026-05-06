using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.Models.Identity;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IIncidentService _incidentService;
    private readonly IAssetService _assetService;
    private readonly IVulnerabilityService _vulnerabilityService;
    private readonly IAuditLogService _auditLogService;

    public AdminController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IIncidentService incidentService,
        IAssetService assetService,
        IVulnerabilityService vulnerabilityService,
        IAuditLogService auditLogService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _incidentService = incidentService;
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
            AssetCount = await _assetService.GetTotalCountAsync(),
            VulnerabilityCount = await _vulnerabilityService.GetTotalCountAsync(),
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
                user.Email != null && user.Email.EndsWith("@cloudsecops.local")) >= expectedRoles.Count
        };

        return View(model);
    }
}
