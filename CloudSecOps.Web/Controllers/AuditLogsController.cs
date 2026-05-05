using CloudSecOps.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Admin,Auditor")]
public class AuditLogsController : Controller
{
    private readonly IAuditLogService _auditLogService;

    public AuditLogsController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index()
    {
        var auditLogs = await _auditLogService.GetRecentAsync(50);
        return View(auditLogs);
    }

    public IActionResult Details(Guid id) => View();
}
