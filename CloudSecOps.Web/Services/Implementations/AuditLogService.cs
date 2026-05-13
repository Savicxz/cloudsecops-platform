using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Audit;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.AuditLogs;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Services.Implementations;

public class AuditLogService : IAuditLogService
{
    private readonly AppDbContext _dbContext;

    public AuditLogService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> GetTotalCountAsync() => _dbContext.AuditLogs.CountAsync();

    public async Task<IReadOnlyList<AuditLogListItemViewModel>> GetRecentAsync(int count)
    {
        return await _dbContext.AuditLogs
            .AsNoTracking()
            .OrderByDescending(log => log.Timestamp)
            .Take(count)
            .Select(log => new AuditLogListItemViewModel
            {
                Id = log.Id,
                Action = log.Action,
                EntityName = log.EntityName,
                UserName = log.User != null ? log.User.FullName : "System",
                Timestamp = log.Timestamp
            })
            .ToListAsync();
    }

    public async Task<AuditLogDetailsViewModel?> GetDetailsAsync(Guid id)
    {
        return await _dbContext.AuditLogs
            .AsNoTracking()
            .Where(log => log.Id == id)
            .Select(log => new AuditLogDetailsViewModel
            {
                Id = log.Id,
                Action = log.Action,
                EntityName = log.EntityName,
                EntityId = log.EntityId,
                Details = log.Details,
                UserName = log.User != null ? log.User.FullName : "System",
                Timestamp = log.Timestamp
            })
            .FirstOrDefaultAsync();
    }

    public async Task RecordAsync(string action, string entityName, string entityId, string details, string? userId)
    {
        _dbContext.AuditLogs.Add(new AuditLog
        {
            Id = Guid.NewGuid(),
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            Details = details,
            UserId = userId,
            Timestamp = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();
    }
}
