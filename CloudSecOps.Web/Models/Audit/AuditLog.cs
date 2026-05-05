using CloudSecOps.Web.Models.Identity;

namespace CloudSecOps.Web.Models.Audit;

public class AuditLog
{
    public Guid Id { get; set; }

    public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string EntityId { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
