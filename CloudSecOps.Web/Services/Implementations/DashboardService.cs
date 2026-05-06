using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _dbContext;

    public DashboardService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        return new DashboardViewModel
        {
            OpenIncidents = await _dbContext.Incidents.CountAsync(incident => incident.Status != IncidentStatus.Closed),
            CriticalVulnerabilities = await _dbContext.Vulnerabilities.CountAsync(vulnerability =>
                vulnerability.Status != VulnerabilityStatus.Fixed
                && vulnerability.Status != VulnerabilityStatus.Closed
                && vulnerability.Severity == SeverityLevel.Critical),
            ActiveAssets = await _dbContext.Assets.CountAsync(asset => asset.Status == "Active"),
            RecentAuditEvents = await _dbContext.AuditLogs.CountAsync()
        };
    }
}
