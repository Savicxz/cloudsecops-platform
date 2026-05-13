namespace CloudSecOps.Web.ViewModels.AuditLogs;

public class AuditLogDetailsViewModel
{
    public Guid Id { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string EntityId { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }
}
