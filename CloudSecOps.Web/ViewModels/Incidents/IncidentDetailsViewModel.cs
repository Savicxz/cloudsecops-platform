using CloudSecOps.Web.Models.Enums;

namespace CloudSecOps.Web.ViewModels.Incidents;

public class IncidentDetailsViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IncidentCategory Category { get; set; }

    public SeverityLevel Severity { get; set; }

    public IncidentStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string RelatedAssetName { get; set; } = string.Empty;

    public string AssignedToName { get; set; } = string.Empty;

    public IReadOnlyList<string> Updates { get; set; } = Array.Empty<string>();
}
