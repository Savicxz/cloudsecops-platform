using CloudSecOps.Web.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace CloudSecOps.Web.Data.SeedData;

public static class DemoUserSeeder
{
    private const string DemoPassword = "CloudSecOps@123";

    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        var users = new[]
        {
            new DemoUser("admin@cloudsecops.local", "CloudSecOps Administrator", "Security Operations", "Administrator", "Administrator"),
            new DemoUser("analyst@cloudsecops.local", "CloudSecOps Security Analyst", "Security Operations", "Security Analyst", "SecurityAnalyst"),
            new DemoUser("manager@cloudsecops.local", "CloudSecOps Manager", "Security Operations", "SOC Manager", "Manager"),
            new DemoUser("auditor@cloudsecops.local", "CloudSecOps Auditor", "Governance", "Security Auditor", "Auditor"),
            new DemoUser("reporter@cloudsecops.local", "CloudSecOps Reporter", "IT Operations", "Incident Reporter", "Reporter")
        };

        foreach (var demoUser in users)
        {
            var user = await userManager.FindByEmailAsync(demoUser.Email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = demoUser.Email,
                    Email = demoUser.Email,
                    EmailConfirmed = true,
                    FullName = demoUser.FullName,
                    Department = demoUser.Department,
                    JobTitle = demoUser.JobTitle
                };

                await userManager.CreateAsync(user, DemoPassword);
            }

            if (!await userManager.IsInRoleAsync(user, demoUser.Role))
            {
                await userManager.AddToRoleAsync(user, demoUser.Role);
            }
        }
    }

    private sealed record DemoUser(
        string Email,
        string FullName,
        string Department,
        string JobTitle,
        string Role);
}
