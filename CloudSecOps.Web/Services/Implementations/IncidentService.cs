using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.Models.Incidents;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Incidents;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Services.Implementations;

public class IncidentService : IIncidentService
{
    private readonly AppDbContext _dbContext;
    private readonly IAuditLogService _auditLogService;

    public IncidentService(AppDbContext dbContext, IAuditLogService auditLogService)
    {
        _dbContext = dbContext;
        _auditLogService = auditLogService;
    }

    public Task<int> GetTotalCountAsync() => _dbContext.Incidents.CountAsync();

    public Task<int> GetOpenCountAsync() =>
        _dbContext.Incidents.CountAsync(incident => incident.Status != IncidentStatus.Closed);

    public Task<int> GetHighSeverityOpenCountAsync() =>
        _dbContext.Incidents.CountAsync(incident =>
            incident.Status != IncidentStatus.Closed
            && (incident.Severity == SeverityLevel.High || incident.Severity == SeverityLevel.Critical));

    public async Task<IReadOnlyList<IncidentListItemViewModel>> GetRecentAsync(int count)
    {
        return await ProjectListItems(_dbContext.Incidents)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<IncidentListItemViewModel>> GetAssignedOpenAsync(string? analystUserId, int count)
    {
        var query = _dbContext.Incidents
            .Where(incident => incident.Status != IncidentStatus.Closed);

        if (!string.IsNullOrWhiteSpace(analystUserId))
        {
            query = query.Where(incident => incident.AssignedToUserId == analystUserId);
        }

        return await ProjectListItems(query)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<IncidentListItemViewModel>> GetHighSeverityOpenAsync(int count)
    {
        return await ProjectListItems(_dbContext.Incidents.Where(incident =>
                incident.Status != IncidentStatus.Closed
                && (incident.Severity == SeverityLevel.High || incident.Severity == SeverityLevel.Critical)))
            .Take(count)
            .ToListAsync();
    }

    public async Task<IncidentDetailsViewModel?> GetDetailsAsync(Guid id)
    {
        return await _dbContext.Incidents
            .AsNoTracking()
            .Include(incident => incident.RelatedAsset)
            .Include(incident => incident.AssignedToUser)
            .Include(incident => incident.ReportedByUser)
            .Include(incident => incident.Updates)
            .Where(incident => incident.Id == id)
            .Select(incident => new IncidentDetailsViewModel
            {
                Id = incident.Id,
                Title = incident.Title,
                Description = incident.Description,
                Status = incident.Status,
                Updates = incident.Updates
                    .OrderByDescending(update => update.CreatedAt)
                    .Select(update => string.IsNullOrWhiteSpace(update.Comment)
                        ? $"{update.CreatedAt:u}: status changed to {update.NewStatus}"
                        : $"{update.CreatedAt:u}: {update.Comment}")
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public Task CreateAsync(IncidentFormViewModel model)
    {
        var incident = new Incident
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Description = model.Description,
            Category = model.Category,
            Severity = model.Severity,
            RelatedAssetId = model.RelatedAssetId,
            Status = IncidentStatus.Reported,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Incidents.Add(incident);
        return _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, IncidentFormViewModel model)
    {
        var incident = await _dbContext.Incidents.FindAsync(id);
        if (incident == null)
        {
            return;
        }

        incident.Title = model.Title;
        incident.Description = model.Description;
        incident.Category = model.Category;
        incident.Severity = model.Severity;
        incident.RelatedAssetId = model.RelatedAssetId;
        incident.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateStatusAsync(Guid id, IncidentStatus status, string? note, string? userId)
    {
        var incident = await _dbContext.Incidents.FindAsync(id);
        if (incident == null)
        {
            return false;
        }

        var oldStatus = incident.Status;
        incident.Status = status;
        incident.UpdatedAt = DateTime.UtcNow;
        incident.ClosedAt = status == IncidentStatus.Closed ? DateTime.UtcNow : null;

        _dbContext.IncidentUpdates.Add(new IncidentUpdate
        {
            Id = Guid.NewGuid(),
            IncidentId = incident.Id,
            UpdatedByUserId = userId,
            OldStatus = oldStatus,
            NewStatus = status,
            Comment = note?.Trim() ?? string.Empty,
            CreatedAt = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();
        await _auditLogService.RecordAsync(
            "Incident status updated",
            nameof(Incident),
            incident.Id.ToString(),
            $"{oldStatus} -> {status}",
            userId);

        return true;
    }

    private static IQueryable<IncidentListItemViewModel> ProjectListItems(IQueryable<Incident> query)
    {
        return query
            .AsNoTracking()
            .OrderByDescending(incident => incident.CreatedAt)
            .Select(incident => new IncidentListItemViewModel
            {
                Id = incident.Id,
                Title = incident.Title,
                Category = incident.Category,
                Severity = incident.Severity,
                Status = incident.Status,
                CreatedAt = incident.CreatedAt
            });
    }
}
