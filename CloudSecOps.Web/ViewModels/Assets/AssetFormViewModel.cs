using CloudSecOps.Web.Models.Enums;

namespace CloudSecOps.Web.ViewModels.Assets;

public class AssetFormViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public AssetType AssetType { get; set; }

    public AssetEnvironment Environment { get; set; }

    public string Owner { get; set; } = string.Empty;

    public AssetCriticality Criticality { get; set; }
}
