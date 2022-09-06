using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Business.Services.AlphaVantage.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class AlphaVantageAssetHistory
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, DayReport> TimeSeries { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MetaData
    {
        [JsonProperty("1. Information")]
        public string Information { get; set; }

        [JsonProperty("2. Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("3. Last Refreshed")]
        public string LastRefreshed { get; set; }

        [JsonProperty("4. Output Size")]
        public string OutputSize { get; set; }

        [JsonProperty("5. Time Zone")]
        public string TimeZone { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class DayReport
    {
        [JsonProperty("1. open")]
        public string Open { get; set; }

        [JsonProperty("2. high")]
        public string High { get; set; }

        [JsonProperty("3. low")]
        public string Low { get; set; }

        [JsonProperty("4. close")]
        public string Close { get; set; }

        [JsonProperty("5. volume")]
        public string Volume { get; set; }
    }
}
