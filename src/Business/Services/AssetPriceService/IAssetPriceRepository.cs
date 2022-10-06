using Business.Models;

namespace Business.Services.AssetPriceService
{
    public interface IAssetPriceRepository
    {
        Task<AssetPrice> CreateAssetPrice( AssetPrice asset );
        Task<AssetPrice> GetAssetPrice( string ticker );
    }
}
