using CloudSecOps.Web.Models.Assets;
using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.Models.Identity;

namespace CloudSecOps.Web.Models.Incidents;

public class Incident
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IncidentCategory Category { get; set; }

    public SeverityLevel Severity { get; set; }

    public IncidentStatus Status { get; set; } = IncidentStatus.Reported;

    public string? ReportedByUserId { get; set; }

    public ApplicationUser? ReportedByUser { get; set; }

    public string? AssignedToUserId { get; set; }

    public ApplicationUser? AssignedToUser { get; set; }

    public Guid? RelatedAssetId { get; set; }

    public Asset? RelatedAsset { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public ICollection<IncidentUpdate> Updates { get; set; } = new List<IncidentUpdate>();
}
