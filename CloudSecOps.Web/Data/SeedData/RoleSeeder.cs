using CloudSecOps.Web.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace CloudSecOps.Web.Data.SeedData;

public static class RoleSeeder
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in Enum.GetNames<UserRoles>())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
