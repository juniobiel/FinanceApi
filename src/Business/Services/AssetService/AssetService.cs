using Business.Models;
using Business.Models.Enums;
using Business.Services.AlphaVantage;
using Business.Services.AlphaVantage.ViewModels;
using Microsoft.AspNetCore.Http;
using UnitTests.Services;

namespace Business.Services.AssetService
{
    public class AssetService : IAssetService
    {
        readonly IAssetRepository _assetRepository;
        readonly IAlphaVantageService _alphaVantageService;
        private string ApiKey;

        public AssetService(IAssetRepository assetRepository, 
            IAlphaVantageService alphaVantageService)
        {
            _assetRepository = assetRepository;
            _alphaVantageService = alphaVantageService;
        }

        public async Task<int> CreateAsset( string ticker )
        {
            var searchResult = await _alphaVantageService.SearchAsset(ticker);

            if (searchResult == null)
                return StatusCodes.Status400BadRequest;

            if (searchResult.BestMatches.Count > 1)
                return StatusCodes.Status400BadRequest;

            var filteredAsset = FilterAsset(searchResult);
            
            if (!filteredAsset.Ticker.Equals(ticker))
                return StatusCodes.Status400BadRequest;

            var createResult = await _assetRepository.CreateNewAsset(filteredAsset);

            if (createResult == 200)
            {
                await UpdateAssetCurrentPrice(ticker);
                return StatusCodes.Status200OK;
            }                
            else
                return StatusCodes.Status400BadRequest;
        }

        private Asset FilterAsset(AlphaVantageSearchResult searchResult)
        {
            var listToFilter = new List<string>
            {
                "Imobiliario",
                "FII",
            };

            var result = searchResult.BestMatches[0];
            result.symbol = result.symbol.Replace(".SAO", string.Empty);

            Asset asset = new()
            {
                Ticker = result.symbol
            };


            foreach (var filter in listToFilter)
            {
                if (result.type == "ETF")
                {
                    if (result.name.Contains(filter))
                    {
                        asset.AssetType = AssetType.FII;
                        break;
                    }
                    else
                        asset.AssetType = AssetType.ETF;
                }
            }

            if (result.type == "Equity")
                asset.AssetType = AssetType.Acao;

            return asset;
        }

        public async Task UpdateAssetCurrentPrice(string ticker)
        {
            var result = await _alphaVantageService.GetAssetHistory(ticker);

        }
    }
}
