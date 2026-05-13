using CloudSecOps.Web.ViewModels.Assets;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IAssetService
{
    Task<int> GetTotalCountAsync();

    Task<IReadOnlyList<AssetListItemViewModel>> GetRecentAsync(int count);

    Task<int> GetActiveCountAsync();

    Task<AssetDetailsViewModel?> GetDetailsAsync(Guid id);

    Task<AssetFormViewModel?> GetFormAsync(Guid id);

    Task CreateAsync(AssetFormViewModel model);

    Task UpdateAsync(Guid id, AssetFormViewModel model);
}
