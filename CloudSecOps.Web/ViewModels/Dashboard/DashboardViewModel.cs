namespace CloudSecOps.Web.ViewModels.Dashboard;

public class DashboardViewModel
{
    public int OpenIncidents { get; set; }

    public int CriticalVulnerabilities { get; set; }

    public int ActiveAssets { get; set; }

    public int RecentAuditEvents { get; set; }
}
