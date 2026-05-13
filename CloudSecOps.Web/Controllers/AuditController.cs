using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Auditor")]
public class AuditController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IAuditLogService _auditLogService;

    public AuditController(AppDbContext dbContext, IAuditLogService auditLogService)
    {
        _dbContext = dbContext;
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AuditDashboardViewModel
        {
            AuditLogCount = await _dbContext.AuditLogs.CountAsync(),
            CompletedIncidentCount = await _dbContext.Incidents.CountAsync(incident => incident.Status == IncidentStatus.Closed),
            EvidenceFileCount = await _dbContext.EvidenceFiles.CountAsync(),
            RecentAuditLogs = await _auditLogService.GetRecentAsync(5)
        };

        return View(model);
    }

    public async Task<IActionResult> CompletedIncidents()
    {
        var incidents = await _dbContext.Incidents
            .AsNoTracking()
            .Where(incident => incident.Status == IncidentStatus.Closed)
            .OrderByDescending(incident => incident.ClosedAt ?? incident.UpdatedAt ?? incident.CreatedAt)
            .ToListAsync();

        return View(incidents);
    }

    public async Task<IActionResult> EvidenceReview()
    {
        var evidence = await _dbContext.EvidenceFiles
            .AsNoTracking()
            .Include(file => file.Incident)
            .Include(file => file.Vulnerability)
            .Include(file => file.UploadedByUser)
            .OrderByDescending(file => file.UploadedAt)
            .ToListAsync();

        return View(evidence);
    }
}
