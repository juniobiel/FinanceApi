using Business.Models;
using UnitTests.Services;

namespace Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        public Task<int> CreateNewAsset( Asset asset )
        {
            throw new NotImplementedException();
        }

        public Task<Asset> GetAsset( string ticker )
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsset( Asset asset )
        {
            throw new NotImplementedException();
        }
    }
}
