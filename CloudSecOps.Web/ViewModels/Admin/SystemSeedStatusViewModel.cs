namespace CloudSecOps.Web.ViewModels.Admin;

public class SystemSeedStatusViewModel
{
    public bool RolesSeeded { get; set; }

    public bool DemoUsersSeeded { get; set; }

    public IReadOnlyList<string> ExpectedRoles { get; set; } = Array.Empty<string>();

    public IReadOnlyList<string> MissingRoles { get; set; } = Array.Empty<string>();

    public int DemoUserCount { get; set; }
}
