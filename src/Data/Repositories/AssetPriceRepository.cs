using Business.Models;
using Business.Services.AssetPriceService;
using System.Diagnostics.CodeAnalysis;

namespace Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AssetPriceRepository : IAssetPriceRepository
    {
        public Task<AssetPrice> CreateAssetPrice( AssetPrice asset )
        {
            throw new NotImplementedException();
        }

        public Task<AssetPrice> GetAssetPrice( string ticker )
        {
            throw new NotImplementedException();
        }
    }
}
