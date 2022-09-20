using Business.Models;
using Business.Services.AssetService;
using System.Diagnostics.CodeAnalysis;

namespace Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AssetRepository : IAssetRepository
    {
        public Task<AssetPrice> CreateNewAsset( AssetPrice asset )
        {
            throw new NotImplementedException();
        }

        public Task<AssetPrice> GetAsset( string ticker )
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsset( AssetPrice asset )
        {
            throw new NotImplementedException();
        }
    }
}
