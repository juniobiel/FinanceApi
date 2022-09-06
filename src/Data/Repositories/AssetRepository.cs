using Business.Models;
using System.Diagnostics.CodeAnalysis;
using UnitTests.Services;

namespace Data.Repositories
{
    [ExcludeFromCodeCoverage]
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
