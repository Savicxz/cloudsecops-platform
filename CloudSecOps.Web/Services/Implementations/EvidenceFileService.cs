using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.EvidenceFiles;

namespace CloudSecOps.Web.Services.Implementations;

public class EvidenceFileService : IEvidenceFileService
{
    public Task<int> GetTotalCountAsync() => Task.FromResult(0);

    public Task<IReadOnlyList<EvidenceFileViewModel>> GetRecentAsync(int count) =>
        Task.FromResult<IReadOnlyList<EvidenceFileViewModel>>(Array.Empty<EvidenceFileViewModel>());
}
