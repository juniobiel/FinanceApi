using Business.Models;

namespace Business.Services.AssetService
{
    public interface IAssetPriceRepository
    {
        Task<AssetPrice> CreateAssetPrice( AssetPrice asset );
        Task<AssetPrice> GetAssetPrice(string ticker);
    }
}
