using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Dashboard;

namespace CloudSecOps.Web.Services.Implementations;

public class DashboardService : IDashboardService
{
    public Task<DashboardViewModel> GetDashboardAsync()
    {
        var model = new DashboardViewModel
        {
            OpenIncidents = 0,
            CriticalVulnerabilities = 0,
            ActiveAssets = 0,
            RecentAuditEvents = 0
        };

        return Task.FromResult(model);
    }
}
