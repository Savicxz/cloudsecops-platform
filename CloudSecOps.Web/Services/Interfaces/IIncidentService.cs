using CloudSecOps.Web.ViewModels.Incidents;
using CloudSecOps.Web.Models.Enums;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IIncidentService
{
    Task<int> GetTotalCountAsync();

    Task<int> GetOpenCountAsync();

    Task<int> GetHighSeverityOpenCountAsync();

    Task<IReadOnlyList<IncidentListItemViewModel>> GetRecentAsync(int count);

    Task<IReadOnlyList<IncidentListItemViewModel>> GetOpenAsync(int count);

    Task<IReadOnlyList<IncidentListItemViewModel>> GetAssignedOpenAsync(string? analystUserId, int count);

    Task<IReadOnlyList<IncidentListItemViewModel>> GetHighSeverityOpenAsync(int count);

    Task<IncidentDetailsViewModel?> GetDetailsAsync(Guid id);

    Task CreateAsync(IncidentFormViewModel model);

    Task UpdateAsync(Guid id, IncidentFormViewModel model);

    Task<bool> UpdateStatusAsync(Guid id, IncidentStatus status, string? note, string? userId);
}
