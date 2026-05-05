using CloudSecOps.Web.ViewModels.EvidenceFiles;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IEvidenceFileService
{
    Task<int> GetTotalCountAsync();

    Task<IReadOnlyList<EvidenceFileViewModel>> GetRecentAsync(int count);
}
