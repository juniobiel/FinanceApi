using Business.Models;

namespace Business.Services.AssetService
{
    public interface IAssetPriceService
    {
        Task<AssetPrice> GetOrCreateAssetPrice( string ticker );

    }
}
