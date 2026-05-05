using CloudSecOps.Web.Models.Enums;

namespace CloudSecOps.Web.Models.Assets;

public class Asset
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public AssetType AssetType { get; set; }

    public AssetEnvironment Environment { get; set; }

    public string Owner { get; set; } = string.Empty;

    public AssetCriticality Criticality { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
