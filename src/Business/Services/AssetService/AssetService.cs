using Business.Interfaces;
using Business.Models;
using Business.Models.Enums;
using Business.Services.AlphaVantage;
using Business.Services.AlphaVantage.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Business.Services.AssetService
{
    public class AssetService : IAssetService
    {
        readonly IAssetRepository _assetRepository;
        readonly IAlphaVantageService _alphaVantageService;
        readonly IUser _appUser;

        public AssetService( IAssetRepository assetRepository,
            IAlphaVantageService alphaVantageService,
            IUser appUser )
        {
            _assetRepository = assetRepository;
            _alphaVantageService = alphaVantageService;
            _appUser = appUser;
        }

        public async Task<AssetPrice> CreateAsset( string ticker )
        {
            var searchResult = await _alphaVantageService.SearchAsset(ticker);
            if (searchResult.BestMatches.Count == 0)
                return null;

            var filteredAsset = FilterAsset(searchResult);

            var createResult = await _assetRepository.CreateNewAsset(filteredAsset);
            if (createResult == null)
                return null;

            var priceUpdate = await UpdateAssetCurrentPrice(ticker);
            if (priceUpdate != 200)
                return null;

            return createResult;
        }

        private static AssetPrice FilterAsset( AlphaVantageSearchResult searchResult )
        {
            var listToFilter = new List<string>
            {
                "Imobiliario",
                "FII",
            };

            var result = searchResult.BestMatches[0];
            result.symbol = result.symbol.Replace(".SAO", string.Empty);

            AssetPrice asset = new()
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
                else
                    break;
            }

            if (result.type == "Equity")
                asset.AssetType = AssetType.Acao;

            return asset;
        }

        public async Task<int> UpdateAssetCurrentPrice( string ticker )
        {
            var result = await _alphaVantageService.GetAssetHistory(ticker);

            var asset = await _assetRepository.GetAsset(ticker);
            var lastDay = DateTime.Now.Date;

            result.TimeSeries.TryGetValue(lastDay.ToString(), out DayReport lastDayReport);

            if (lastDayReport == null)
            {
                lastDay = lastDay.AddDays(-1);
                result.TimeSeries.TryGetValue(lastDay.ToString(), out lastDayReport);
            }


            asset.CurrentPrice = double.Parse(lastDayReport.Close, CultureInfo.InvariantCulture);
            asset.UpdatedAt = DateTime.Now;
            asset.UpdatedByUser = _appUser.GetUserId();

            var assetUpdate = await _assetRepository.UpdateAsset(asset);
            if (assetUpdate != 200)
                return StatusCodes.Status400BadRequest;

            return StatusCodes.Status200OK;
        }

        public Task<AssetPrice> GetAsset( string ticker )
        {
            throw new NotImplementedException();
        }
    }
}
