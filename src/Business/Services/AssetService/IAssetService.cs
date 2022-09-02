using Business.Services.AlphaVantage.ViewModels;

namespace Business.Services.AssetService
{
    public interface IAssetService
    {
        Task<int> CreateAsset(string ticker);
        Task UpdateAssetCurrentPrice(string ticker);
    }
}
