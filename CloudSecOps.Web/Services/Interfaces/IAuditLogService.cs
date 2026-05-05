using CloudSecOps.Web.ViewModels.AuditLogs;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IAuditLogService
{
    Task<int> GetTotalCountAsync();

    Task<IReadOnlyList<AuditLogListItemViewModel>> GetRecentAsync(int count);
}
