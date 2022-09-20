using Business.Services.AlphaVantage.ViewModels;
using Business.Services.ServiceKey;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Business.Services.AlphaVantage
{
    [ExcludeFromCodeCoverage]
    public class AlphaVantageService : IAlphaVantageService
    {
        readonly IServiceKey _serviceKey;

        public AlphaVantageService( IConfiguration configuration, IServiceKey serviceKey )
        {
            _serviceKey = serviceKey;
            _serviceKey.AlphaVantageKey = configuration["AlphaVantage_ApiKey"];
        }

        public async Task<AlphaVantageSearchResult> SearchAsset( string ticker )
        {
            var uri = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={ticker}.SAO&apikey={_serviceKey.AlphaVantageKey}";
            var result = await uri.GetStringAsync();
            var json = JsonConvert.DeserializeObject<AlphaVantageSearchResult>(result);

            return json;
        }

        public async Task<AlphaVantageAssetHistory> GetAssetHistory( string ticker )
        {
            var uri = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={ticker}.SAO&outputsize=full&apikey={_serviceKey.AlphaVantageKey}";
            var result = await uri.GetStringAsync();
            var json = JsonConvert.DeserializeObject<AlphaVantageAssetHistory>(result);

            return json;
        }

        public async Task<double> GetAssetLastPrice(string ticker)
        {
            var result = await GetAssetHistory(ticker);

            var lastDay = DateTime.Now.Date;

            result.TimeSeries.TryGetValue(lastDay.ToString(), out DayReport lastDayReport);

            if (lastDayReport == null)
            {
                lastDay = lastDay.AddDays(-1);
                result.TimeSeries.TryGetValue(lastDay.ToString(), out lastDayReport);
            }

            return double.Parse(lastDayReport.Close);
        }
    }
}
