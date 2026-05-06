using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.ViewModels.Incidents;

namespace CloudSecOps.Web.ViewModels.Analyst;

public class AnalystIncidentReviewViewModel
{
    public IncidentDetailsViewModel Incident { get; set; } = new();

    public IncidentStatus NewStatus { get; set; }

    public string? InvestigationNote { get; set; }

    public IReadOnlyList<IncidentStatus> AvailableStatuses { get; set; } =
        Enum.GetValues<IncidentStatus>();
}
