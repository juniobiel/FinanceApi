using Business.Models;
using Business.Services.AlphaVantage;
using Business.Services.AlphaVantage.ViewModels;
using Business.Services.AssetService;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.AutoMock;

namespace UnitTests.Services
{
    public class AssetServiceTests
    {
        readonly AutoMocker _mocker;
        readonly AssetService _service;
        public AssetServiceTests()
        {
            _mocker = new AutoMocker();
            _service = _mocker.WithSelfMock<AssetService>();
        }


        [Fact(DisplayName = "Criar um asset e tipar para FII")]
        [Trait("create-asset", "Status 200")]
        public async void CreateAsset_WhenCalled_ReturnCode200()
        {
            //Arrange
            var searchResult = new AlphaVantageSearchResult
            {
                BestMatches = new List<AlphaVantageAssetInformation>
                {
                    new AlphaVantageAssetInformation
                    {
                        symbol = "BRCO11.SAO",
                        name = "Bresco - Fundo De Investimento Imobiliario",
                        type = "ETF",
                        region = "Brazil/Sao Paolo",
                        marketOpen = "10:00",
                        marketClose = "17:30",
                        timezone = "UTC-03",
                        currency = "BRL",
                        matchScore =  "1.0000"
                    }
                }
            };

            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.SearchAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(searchResult));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.CreateNewAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));
            //Act
            var teste1 = new AlphaVantageService(_mocker.GetMock<IConfiguration>().Object);
            var teste = await teste1.GetAssetHistory("BRCO11");
            var result = await _service.CreateAsset("BRCO11");



            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(200, result);
        }
    }
}
