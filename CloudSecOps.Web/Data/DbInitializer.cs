namespace CloudSecOps.Web.Data;

using CloudSecOps.Web.Data.SeedData;
using CloudSecOps.Web.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleSeeder.SeedAsync(roleManager);

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await DemoUserSeeder.SeedAsync(userManager);
    }
}
