using CloudSecOps.Web.ViewModels.AuditLogs;

namespace CloudSecOps.Web.ViewModels.Audit;

public class AuditDashboardViewModel
{
    public int AuditLogCount { get; set; }

    public int CompletedIncidentCount { get; set; }

    public int EvidenceFileCount { get; set; }

    public IReadOnlyList<AuditLogListItemViewModel> RecentAuditLogs { get; set; } =
        Array.Empty<AuditLogListItemViewModel>();
}
