namespace CloudSecOps.Web.ViewModels.Admin;

public class SystemSeedStatusViewModel
{
    public bool RolesSeeded { get; set; }

    public bool DemoUsersSeeded { get; set; }

    public IReadOnlyList<string> ExpectedRoles { get; set; } = Array.Empty<string>();

    public IReadOnlyList<string> MissingRoles { get; set; } = Array.Empty<string>();

    public int DemoUserCount { get; set; }

    public int UserCount { get; set; }

    public int RoleCount { get; set; }

    public int IncidentCount { get; set; }

    public int IncidentReportCount { get; set; }

    public int AuditLogCount { get; set; }
}
