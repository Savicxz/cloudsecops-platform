using CloudSecOps.Web.Models.Enums;

namespace CloudSecOps.Web.ViewModels.Incidents;

public class IncidentFormViewModel
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IncidentCategory Category { get; set; }

    public SeverityLevel Severity { get; set; }

    public Guid? RelatedAssetId { get; set; }
}
