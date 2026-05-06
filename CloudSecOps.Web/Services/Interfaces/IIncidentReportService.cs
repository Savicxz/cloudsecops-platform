using CloudSecOps.Web.ViewModels.Analyst;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IIncidentReportService
{
    Task<int> GetTotalCountAsync();

    Task<int> GetOpenCountAsync();

    Task<IReadOnlyList<AnalystIncidentReportListItemViewModel>> GetRecentAsync(int count);

    Task<AnalystIncidentReportReviewViewModel?> GetDetailsAsync(int id);

    Task<bool> UpdateStatusAsync(int id, string status, string? reviewNote, string? userId);
}
