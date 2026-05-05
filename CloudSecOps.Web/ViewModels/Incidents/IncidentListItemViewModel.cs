using CloudSecOps.Web.Models.Enums;

namespace CloudSecOps.Web.ViewModels.Incidents;

public class IncidentListItemViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public IncidentCategory Category { get; set; }

    public SeverityLevel Severity { get; set; }

    public IncidentStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}
