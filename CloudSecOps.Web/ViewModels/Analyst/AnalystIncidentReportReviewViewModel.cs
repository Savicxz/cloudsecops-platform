namespace CloudSecOps.Web.ViewModels.Analyst;

public class AnalystIncidentReportReviewViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Severity { get; set; } = string.Empty;

    public string AffectedAsset { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string ReporterName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public string NewStatus { get; set; } = string.Empty;

    public string? ReviewNote { get; set; }

    public IReadOnlyList<string> AvailableStatuses { get; set; } =
        ["Submitted", "Assigned", "Investigating", "Resolved", "Closed"];
}
