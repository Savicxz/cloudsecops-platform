using CloudSecOps.Web.ViewModels.AuditLogs;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IAuditLogService
{
    Task<int> GetTotalCountAsync();

    Task<IReadOnlyList<AuditLogListItemViewModel>> GetRecentAsync(int count);

    Task<AuditLogDetailsViewModel?> GetDetailsAsync(Guid id);

    Task RecordAsync(string action, string entityName, string entityId, string details, string? userId);
}
