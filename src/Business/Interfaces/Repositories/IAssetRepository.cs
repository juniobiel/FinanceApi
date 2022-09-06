using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IAssetRepository
    {
        Task<Asset> CreateNewAsset( Asset asset );
        Task<Asset> GetAsset( string ticker );
        Task<int> UpdateAsset( Asset asset );
    }
}
