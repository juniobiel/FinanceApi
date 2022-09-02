using Business.Services.AlphaVantage.ViewModels;

namespace Business.Services.AlphaVantage
{
    public interface IAlphaVantageService
    {
        Task<AlphaVantageSearchResult> SearchAsset( string ticker );
        Task<AlphaVantageAssetHistory> GetAssetHistory( string ticker );
    }
}
