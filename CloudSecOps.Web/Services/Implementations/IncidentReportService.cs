using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Incidents;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Analyst;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Services.Implementations;

public class IncidentReportService : IIncidentReportService
{
    private static readonly string[] OpenStatuses = ["Open", "Reported", "Submitted", "Assigned", "Investigating"];
    private static readonly string[] ReviewStatuses = ["Submitted", "Reported", "Assigned", "Investigating", "Resolved", "Closed"];

    private readonly AppDbContext _dbContext;
    private readonly IAuditLogService _auditLogService;

    public IncidentReportService(AppDbContext dbContext, IAuditLogService auditLogService)
    {
        _dbContext = dbContext;
        _auditLogService = auditLogService;
    }

    public Task<int> GetTotalCountAsync() => _dbContext.IncidentReports.CountAsync();

    public Task<int> GetOpenCountAsync() =>
        _dbContext.IncidentReports.CountAsync(report => OpenStatuses.Contains(report.Status));

    public async Task<IReadOnlyList<AnalystIncidentReportListItemViewModel>> GetRecentAsync(int count)
    {
        return await _dbContext.IncidentReports
            .AsNoTracking()
            .Where(report => report.Status != "Draft")
            .OrderByDescending(report => report.CreatedAt)
            .Take(count)
            .Select(report => new AnalystIncidentReportListItemViewModel
            {
                Id = report.Id,
                Title = report.Title,
                Category = report.Category,
                Severity = report.Severity,
                AffectedAsset = report.AffectedAsset,
                Status = report.Status,
                CreatedAt = report.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<AnalystIncidentReportReviewViewModel?> GetDetailsAsync(int id)
    {
        return await _dbContext.IncidentReports
            .AsNoTracking()
            .Where(report => report.Id == id && report.Status != "Draft")
            .Select(report => new AnalystIncidentReportReviewViewModel
            {
                Id = report.Id,
                Title = report.Title,
                Category = report.Category,
                Severity = report.Severity,
                AffectedAsset = report.AffectedAsset,
                Description = report.Description,
                Status = report.Status,
                ReporterName = report.ReporterName,
                CreatedAt = report.CreatedAt,
                SubmittedAt = report.SubmittedAt,
                NewStatus = report.Status
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateStatusAsync(int id, string status, string? reviewNote, string? userId)
    {
        if (!ReviewStatuses.Contains(status))
        {
            return false;
        }

        var report = await _dbContext.IncidentReports.FindAsync(id);
        if (report == null)
        {
            return false;
        }

        var oldStatus = report.Status;
        report.Status = status;

        await _dbContext.SaveChangesAsync();
        await _auditLogService.RecordAsync(
            "Incident report status updated",
            nameof(IncidentReport),
            report.Id.ToString(),
            string.IsNullOrWhiteSpace(reviewNote)
                ? $"{oldStatus} -> {status}"
                : $"{oldStatus} -> {status}: {reviewNote.Trim()}",
            userId);

        return true;
    }
}
