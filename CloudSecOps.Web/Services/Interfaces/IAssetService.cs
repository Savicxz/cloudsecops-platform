using CloudSecOps.Web.ViewModels.Assets;

namespace CloudSecOps.Web.Services.Interfaces;

public interface IAssetService
{
    Task<int> GetTotalCountAsync();

    Task<IReadOnlyList<AssetListItemViewModel>> GetRecentAsync(int count);

    Task CreateAsync(AssetFormViewModel model);

    Task UpdateAsync(Guid id, AssetFormViewModel model);
}
