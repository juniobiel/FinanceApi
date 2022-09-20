using Business.Models;
using Business.Services.AlphaVantage;
using Business.Services.AssetService;
using Moq;
using Moq.AutoMock;

namespace UnitTests.Services
{
    public class AssetPriceServiceTests
    {
        readonly AutoMocker _mocker;
        readonly IAssetPriceService _service;

        public AssetPriceServiceTests()
        {
            _mocker = new AutoMocker();
            _service = _mocker.CreateInstance<AssetPriceService>();
        }

        [Fact(DisplayName = "Criar nova instancia de AssetPrice")]
        [Trait("CreateAssetPrice", "Success")]
        public async Task GetOrCreateAssetPrice_WhenAssetPriceDoesNotExists_ReturnSucess()
        {
            // Arrange

            _mocker.GetMock<IAssetPriceRepository>()
                .Setup(x => x.CreateAssetPrice(It.IsAny<AssetPrice>()))
                .ReturnsAsync(new AssetPrice
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now.AddDays(-1),
                    LastPrice = 15.1,
                    Ticker = "PETR4",
                });
            _mocker.GetMock<IAlphaVantageService>()
               .Setup(x => x.GetAssetLastPrice(It.IsAny<string>()))
               .ReturnsAsync(15.1);

            // Act
            var result = await _service.GetOrCreateAssetPrice("PETR4");

            // Assert
            Assert.IsType<AssetPrice>(result);
            Assert.Equal(15.1, result.LastPrice );
        }

        [Fact(DisplayName = "Capturar a instancia de AssetPrice")]
        [Trait("GetAssetPrice", "Success")]
        public async Task GetOrCreateAssetPrice_WhenAssetPriceExists_ReturnSucess()
        {
            // Arrange
            _mocker.GetMock<IAssetPriceRepository>()
                .Setup(x => x.GetAssetPrice(It.IsAny<string>()))
                .ReturnsAsync(new AssetPrice
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now.AddDays(-1),
                    LastPrice = 15.1,
                    Ticker = "PETR4",
                });

            // Act
            var result = await _service.GetOrCreateAssetPrice("PETR4");

            // Assert
            Assert.IsType<AssetPrice>(result);
            Assert.Equal(15.1, result.LastPrice);
        }

    }
}
