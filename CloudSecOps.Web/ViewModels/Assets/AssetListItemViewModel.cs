using CloudSecOps.Web.Models.Enums;

namespace CloudSecOps.Web.ViewModels.Assets;

public class AssetListItemViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public AssetType AssetType { get; set; }

    public AssetEnvironment Environment { get; set; }

    public AssetCriticality Criticality { get; set; }

    public string Status { get; set; } = string.Empty;
}
