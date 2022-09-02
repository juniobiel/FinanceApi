using Business.Services.AlphaVantage.ViewModels;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Business.Services.AlphaVantage
{
    public class AlphaVantageService : IAlphaVantageService
    {
        readonly IConfiguration _configuration;
        private string ApiKey { get; set; }

        public AlphaVantageService( IConfiguration configuration )
        {
            _configuration = configuration;

            ApiKey = _configuration["AlphaVantage_ApiKey"];
        }

        public async Task<AlphaVantageSearchResult> SearchAsset( string ticker )
        {
            var uri = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={ticker}.SAO&apikey={ApiKey}";
            var result = await uri.GetStringAsync();
            var json = JsonConvert.DeserializeObject<AlphaVantageSearchResult>(result);

            return json;
        }

        public async Task<AlphaVantageAssetHistory> GetAssetHistory( string ticker )
        {
            var uri = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={ticker}.SAO&outputsize=full&apikey={ApiKey}";
            var result = await uri.GetStringAsync();
            var json = JsonConvert.DeserializeObject<AlphaVantageAssetHistory>(result);

            return json;
        }
    }
}
