using Business.Models;

namespace Business.Services.AssetPriceService
{
    public interface IAssetPriceService
    {
        Task<AssetPrice> GetOrCreateAssetPrice( string ticker );

    }
}
