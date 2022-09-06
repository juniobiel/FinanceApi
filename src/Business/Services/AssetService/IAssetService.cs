using Business.Models;

namespace Business.Services.AssetService
{
    public interface IAssetService
    {
        Task<Asset> CreateAsset( string ticker );
        Task<int> UpdateAssetCurrentPrice(string ticker);
        Task<Asset> GetAsset( string ticker );
    }
}
