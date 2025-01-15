using AssetService.Models;

namespace AssetService.Interfaces
{
    public interface IAssetService
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int id);
        Task<Asset> AddAssetAsync(Asset asset);
        Task<Asset> UpdateAssetAsync(Asset asset);
        Task<bool> DeleteAssetAsync(int id);

    }
}
