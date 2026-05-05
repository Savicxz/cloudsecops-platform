using CloudSecOps.Web.Models.Enums;
using CloudSecOps.Web.Models.Identity;

namespace CloudSecOps.Web.Models.Incidents;

public class IncidentUpdate
{
    public Guid Id { get; set; }

    public Guid IncidentId { get; set; }

    public Incident? Incident { get; set; }

    public string? UpdatedByUserId { get; set; }

    public ApplicationUser? UpdatedByUser { get; set; }

    public string Comment { get; set; } = string.Empty;

    public IncidentStatus? OldStatus { get; set; }

    public IncidentStatus? NewStatus { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
