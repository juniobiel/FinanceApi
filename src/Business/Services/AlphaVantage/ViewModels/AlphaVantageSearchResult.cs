using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Business.Services.AlphaVantage.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class AlphaVantageSearchResult
    {
        [JsonProperty("bestMatches")]
        public List<AlphaVantageAssetInformation> BestMatches { get; set; }
    }
}
