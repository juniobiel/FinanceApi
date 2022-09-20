using Business.Models;

namespace Business.Services.AssetService
{
    public interface IAssetRepository
    {
        Task<AssetPrice> CreateNewAsset(AssetPrice asset);
        Task<AssetPrice> GetAsset(string ticker);
        Task<int> UpdateAsset(AssetPrice asset);
    }
}
