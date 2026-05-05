using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.AuditLogs;

namespace CloudSecOps.Web.Services.Implementations;

public class AuditLogService : IAuditLogService
{
    public Task<int> GetTotalCountAsync() => Task.FromResult(0);

    public Task<IReadOnlyList<AuditLogListItemViewModel>> GetRecentAsync(int count) =>
        Task.FromResult<IReadOnlyList<AuditLogListItemViewModel>>(Array.Empty<AuditLogListItemViewModel>());
}
