using Business.Models;

namespace UnitTests.Services
{
    public interface IAssetRepository
    {
        Task<int> CreateNewAsset( Asset asset );
        Task<Asset> GetAsset( string ticker );
        Task<int> UpdateAsset( Asset asset );
    }
}
