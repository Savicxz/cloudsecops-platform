using CloudSecOps.Web.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace CloudSecOps.Web.Data.SeedData;

public static class DemoUserSeeder
{
    public static Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        // TODO: Add safe demo users for screenshots after password policy is agreed by the team.
        return Task.CompletedTask;
    }
}
