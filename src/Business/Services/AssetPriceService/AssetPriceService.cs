using Business.Interfaces;
using Business.Models;
using Business.Services.AlphaVantage;

namespace Business.Services.AssetPriceService
{
    public class AssetPriceService : IAssetPriceService
    {
        readonly IAssetPriceRepository _repository;
        readonly IAlphaVantageService _alphaVantageService;

        public AssetPriceService( IAssetPriceRepository assetPriceRepository,
            IAlphaVantageService alphaVantageService )
        {
            _repository = assetPriceRepository;
            _alphaVantageService = alphaVantageService;
        }

        public async Task<AssetPrice> GetOrCreateAssetPrice( string ticker )
        {
            var assetPrice = await SearchAssetPrice(ticker);
            assetPrice ??= await CreateAssetPrice(ticker);
            return assetPrice;
        }

        private async Task<AssetPrice> SearchAssetPrice(string ticker)
        {
            return await _repository.GetAssetPrice(ticker);
        }

        private async Task<AssetPrice> CreateAssetPrice( string ticker )
        {
            AssetPrice asset = new()
            {
                Ticker = ticker,
                LastPrice = await _alphaVantageService.GetAssetLastPrice(ticker),
                CreatedAt = DateTime.Now
            };

            return await _repository.CreateAssetPrice(asset);
        }
    }
}
