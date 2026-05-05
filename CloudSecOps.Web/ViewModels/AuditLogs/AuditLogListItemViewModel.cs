namespace CloudSecOps.Web.ViewModels.AuditLogs;

public class AuditLogListItemViewModel
{
    public Guid Id { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }
}
