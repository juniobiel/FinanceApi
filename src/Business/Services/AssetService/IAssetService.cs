using Business.Models;

namespace Business.Services.AssetService
{
    public interface IAssetService
    {
        Task<AssetPrice> CreateAsset( string ticker );
        Task<int> UpdateAssetCurrentPrice(string ticker);
        Task<AssetPrice> GetAsset( string ticker );
    }
}
