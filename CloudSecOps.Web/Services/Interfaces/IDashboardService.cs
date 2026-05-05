using CloudSecOps.Web.ViewModels.Dashboard;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardAsync();
}
