using CloudSecOps.Web.ViewModels.Incidents;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IIncidentService
{
    Task<int> GetTotalCountAsync();

    Task<IReadOnlyList<IncidentListItemViewModel>> GetRecentAsync(int count);

    Task CreateAsync(IncidentFormViewModel model);

    Task UpdateAsync(Guid id, IncidentFormViewModel model);
}
