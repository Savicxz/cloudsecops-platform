using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Incidents;

namespace CloudSecOps.Web.Services.Implementations;

public class IncidentService : IIncidentService
{
    public Task<int> GetTotalCountAsync() => Task.FromResult(0);

    public Task<IReadOnlyList<IncidentListItemViewModel>> GetRecentAsync(int count) =>
        Task.FromResult<IReadOnlyList<IncidentListItemViewModel>>(Array.Empty<IncidentListItemViewModel>());

    public Task CreateAsync(IncidentFormViewModel model)
    {
        // TODO: Persist incident reports and notify assigned analysts.
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Guid id, IncidentFormViewModel model)
    {
        // TODO: Update incident workflow and append IncidentUpdate history.
        return Task.CompletedTask;
    }
}
