using CloudSecOps.Web.ViewModels.AuditLogs;

namespace CloudSecOps.Web.ViewModels.Admin;

public class AdminDashboardViewModel
{
    public int UserCount { get; set; }

    public int RoleCount { get; set; }

    public int IncidentCount { get; set; }

    public int AssetCount { get; set; }

    public int VulnerabilityCount { get; set; }

    public IReadOnlyList<AuditLogListItemViewModel> RecentAuditLogs { get; set; } =
        Array.Empty<AuditLogListItemViewModel>();
}
