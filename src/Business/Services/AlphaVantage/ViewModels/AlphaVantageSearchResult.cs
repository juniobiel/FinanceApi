using Newtonsoft.Json;
using System.Text.Json;

namespace Business.Services.AlphaVantage.ViewModels
{
    public class AlphaVantageSearchResult
    {
        [JsonProperty("bestMatches")]
        public List<AlphaVantageAssetInformation> BestMatches { get; set; }
    }
}
