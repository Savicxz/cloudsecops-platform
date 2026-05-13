using CloudSecOps.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Auditor")]
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

    public async Task<IActionResult> Details(Guid id)
    {
        var log = await _auditLogService.GetDetailsAsync(id);
        if (log == null)
        {
            return NotFound();
        }

        return View(log);
    }
}
