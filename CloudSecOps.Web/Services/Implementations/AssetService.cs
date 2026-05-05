using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Assets;

namespace CloudSecOps.Web.Services.Implementations;

public class AssetService : IAssetService
{
    public Task<int> GetTotalCountAsync() => Task.FromResult(0);

    public Task<IReadOnlyList<AssetListItemViewModel>> GetRecentAsync(int count) =>
        Task.FromResult<IReadOnlyList<AssetListItemViewModel>>(Array.Empty<AssetListItemViewModel>());

    public Task CreateAsync(AssetFormViewModel model)
    {
        // TODO: Persist asset records through AppDbContext.
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Guid id, AssetFormViewModel model)
    {
        // TODO: Update asset records and record audit log entries.
        return Task.CompletedTask;
    }
}
