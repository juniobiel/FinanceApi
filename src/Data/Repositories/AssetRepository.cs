using Business.Interfaces.Repositories;
using Business.Models;
using System.Diagnostics.CodeAnalysis;

namespace Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AssetRepository : IAssetRepository
    {
        public Task<Asset> CreateNewAsset( Asset asset )
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
