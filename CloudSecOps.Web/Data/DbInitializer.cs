namespace CloudSecOps.Web.Data;

public static class DbInitializer
{
    public static Task InitializeAsync(IServiceProvider serviceProvider)
    {
        // TODO: Apply migrations and call seeders once the team finalizes demo accounts.
        return Task.CompletedTask;
    }
}
