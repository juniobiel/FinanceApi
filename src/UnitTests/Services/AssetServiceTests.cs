using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Moq.AutoMock;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;

namespace UnitTests.Services
{

    public interface IAssetRepository
    {
        Task CreateNewAsset( string ticker );
    }

    public interface IAssetService
    {
        Task CreateAsset( string ticker );
        Task<AssetResult> SearchAsset( string ticker );
    }

    public interface IAssetResult { }

    public class AssetResult
    {
        [JsonProperty("bestMatches")]
        public List<Asset> BestMatches { get; set; }
    }

    public class Asset 
    {
        [JsonProperty("1. symbol")]
        public string symbol { get; set; }

        [JsonProperty("2. name")]
        public string name { get; set; }

        [JsonProperty("3. type")]
        public string type { get; set; }

        [JsonProperty("4. region")]
        public string region { get; set; }

        [JsonProperty("5. marketOpen")]
        public string marketOpen { get; set; }

        [JsonProperty("6. marketClose")]
        public string marketClose { get; set; }

        [JsonProperty("7. timezone")]
        public string timezone { get; set; }

        [JsonProperty("8. currency")]
        public string currency { get; set; }

        [JsonProperty("9. matchScore")]
        public string matchScore { get; set; }
    }

    public class AssetService : IAssetService
    {
        readonly IAssetRepository _assetRepository;
        readonly IConfiguration _configuration;
        private string ApiKey;

        public AssetService(IAssetRepository assetRepository, IConfiguration config)
        {
            _assetRepository = assetRepository;
            _configuration = config;
            ApiKey = _configuration["AlphaVantage_ApiKey"];
        }

        public async Task CreateAsset( string ticker )
        {
            var result = await $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={ticker}.SA&apikey={ApiKey}"
                .GetStringAsync();

           var json =  JsonConvert.DeserializeObject<AssetResult>( result );



            Console.Write("teste");
            

        }

        public Task<AssetResult> SearchAsset( string ticker )
        {
            throw new NotImplementedException();
        }
    }

    public class AssetServiceTests
    {
        readonly AutoMocker _mocker;
        readonly AssetService _service;
        public AssetServiceTests()
        {
            _mocker = new AutoMocker();
            _service = _mocker.CreateInstance<AssetService>();
        }


        [Fact(DisplayName = "Criar um Asset e retornar sucesso")]
        [Trait("create-asset", "Status 200")]
        public async void CreateAsset_WhenCalled_ReturnSuccess()
        {
            //Arrange

            //Act
            await _service.CreateAsset("BRCO11");

            //Assert
           // Assert.IsType<ObjectResult>(result);
        }
    }
}
