using Business.Models;
using Business.Services.AssetService;
using Business.Services.UserAssetService;
using Moq;
using Moq.AutoMock;
using System.Net;

namespace UnitTests.Services
{
    public class UserAssetServiceTests
    {
        readonly AutoMocker _mocker;
        readonly UserAssetService _service;
        public UserAssetServiceTests()
        {
            _mocker = new AutoMocker();
            _service = _mocker.CreateSelfMock<UserAssetService>();
        }

        [Fact(DisplayName = "Criar um Asset para o usuário")]
        [Trait("CreateAsset", "Success")]
        public async Task AddToUser_WhenAssetDoesNotExists_ReturnSuccess()
        {
            // Arrange
            Asset asset = new()
            {
                Ticker = "VGHF11",
                Quantity = 2,
                UnitPrice = 9.86
            };

            _mocker.GetMock<IAssetPriceService>()
                .Setup(x => x.GetOrCreateAssetPrice(It.IsAny<string>()))
                .ReturnsAsync(new AssetPrice
                {
                    Id = Guid.NewGuid(),
                    Ticker = "VGHF11",
                    LastPrice = 10
                });
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.CreateUserAsset(It.IsAny<UserAsset>()))
                .ReturnsAsync(HttpStatusCode.OK);

            // Act
            var result = await _service.AddToUserAsset(asset);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Adicionar um Asset para o usuário")]
        [Trait("IncreaseAsset", "Success")]
        public async Task AddToUser_WhenAssetExists_ReturnSuccess()
        {
            // Arrange
            Asset asset = new()
            {
                Ticker = "VGHF11",
                Quantity = 2,
                UnitPrice = 9.86
            };

            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.GetUserAsset(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(new UserAsset());
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.UpdateUserAsset(It.IsAny<UserAsset>()))
                .ReturnsAsync(HttpStatusCode.OK);

            // Act
            var result = await _service.AddToUserAsset(asset);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }
    }
}
