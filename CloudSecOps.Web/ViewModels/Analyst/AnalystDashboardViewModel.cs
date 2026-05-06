using CloudSecOps.Web.ViewModels.Incidents;

namespace CloudSecOps.Web.ViewModels.Analyst;

public class AnalystDashboardViewModel
{
    public int OpenIncidentCount { get; set; }

    public int OpenIncidentReportCount { get; set; }

    public int HighSeverityIncidentCount { get; set; }

    public int AssetCount { get; set; }

    public int HighSeverityVulnerabilityCount { get; set; }

    public IReadOnlyList<IncidentListItemViewModel> AssignedOpenIncidents { get; set; } =
        Array.Empty<IncidentListItemViewModel>();

    public IReadOnlyList<IncidentListItemViewModel> HighSeverityIncidents { get; set; } =
        Array.Empty<IncidentListItemViewModel>();

    public IReadOnlyList<AnalystIncidentReportListItemViewModel> RecentIncidentReports { get; set; } =
        Array.Empty<AnalystIncidentReportListItemViewModel>();
}
