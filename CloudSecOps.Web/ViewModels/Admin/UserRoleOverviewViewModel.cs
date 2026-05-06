namespace CloudSecOps.Web.ViewModels.Admin;

public class UserRoleOverviewViewModel
{
    public IReadOnlyList<UserRoleListItemViewModel> Users { get; set; } =
        Array.Empty<UserRoleListItemViewModel>();

    public IReadOnlyList<string> Roles { get; set; } = Array.Empty<string>();
}
