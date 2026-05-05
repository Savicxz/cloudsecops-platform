using CloudSecOps.Web.Models.Enums;

namespace CloudSecOps.Web.ViewModels.Incidents;

public class IncidentDetailsViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IncidentStatus Status { get; set; }

    public IReadOnlyList<string> Updates { get; set; } = Array.Empty<string>();
}
