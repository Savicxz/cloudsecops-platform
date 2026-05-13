using CloudSecOps.Web.Data;
using CloudSecOps.Web.Models.Assets;
using CloudSecOps.Web.Services.Interfaces;
using CloudSecOps.Web.ViewModels.Assets;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Services.Implementations;

public class AssetService : IAssetService
{
    private readonly AppDbContext _dbContext;

    public AssetService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> GetTotalCountAsync() => _dbContext.Assets.CountAsync();

    public Task<int> GetActiveCountAsync() =>
        _dbContext.Assets.CountAsync(asset => asset.Status == "Active");

    public async Task<IReadOnlyList<AssetListItemViewModel>> GetRecentAsync(int count)
    {
        return await _dbContext.Assets
            .AsNoTracking()
            .OrderByDescending(asset => asset.CreatedAt)
            .Take(count)
            .Select(asset => new AssetListItemViewModel
            {
                Id = asset.Id,
                Name = asset.Name,
                AssetType = asset.AssetType,
                Environment = asset.Environment,
                Criticality = asset.Criticality,
                Status = asset.Status
            })
            .ToListAsync();
    }

    public async Task<AssetDetailsViewModel?> GetDetailsAsync(Guid id)
    {
        return await _dbContext.Assets
            .AsNoTracking()
            .Where(asset => asset.Id == id)
            .Select(asset => new AssetDetailsViewModel
            {
                Id = asset.Id,
                Name = asset.Name,
                Description = asset.Description,
                AssetType = asset.AssetType,
                Environment = asset.Environment,
                Owner = asset.Owner,
                Criticality = asset.Criticality,
                Status = asset.Status,
                CreatedAt = asset.CreatedAt,
                UpdatedAt = asset.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<AssetFormViewModel?> GetFormAsync(Guid id)
    {
        return await _dbContext.Assets
            .AsNoTracking()
            .Where(asset => asset.Id == id)
            .Select(asset => new AssetFormViewModel
            {
                Name = asset.Name,
                Description = asset.Description,
                AssetType = asset.AssetType,
                Environment = asset.Environment,
                Owner = asset.Owner,
                Criticality = asset.Criticality,
                Status = asset.Status
            })
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(AssetFormViewModel model)
    {
        _dbContext.Assets.Add(new Asset
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description,
            AssetType = model.AssetType,
            Environment = model.Environment,
            Owner = model.Owner,
            Criticality = model.Criticality,
            Status = model.Status,
            CreatedAt = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, AssetFormViewModel model)
    {
        var asset = await _dbContext.Assets.FindAsync(id);
        if (asset == null)
        {
            return;
        }

        asset.Name = model.Name;
        asset.Description = model.Description;
        asset.AssetType = model.AssetType;
        asset.Environment = model.Environment;
        asset.Owner = model.Owner;
        asset.Criticality = model.Criticality;
        asset.Status = model.Status;
        asset.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }
}
