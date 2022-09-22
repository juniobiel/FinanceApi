using Business.Models;
using Business.Services.AssetPriceService;
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
        [Trait("Criar Asset", "Success")]
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
        [Trait("Adicionar Asset", "Success")]
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

        [Fact(DisplayName = "Remover uma quantidade de assets do usuário")]
        [Trait("Retirar Assets", "Success")]
        public async Task RemoveToUser_WithAQuantityLessThanTotalQuantity_ReturnSucess()
        {
            //Arrange
            Asset asset = new()
            {
                Ticker = "PETR4",
                UnitPrice = 5.50,
                Quantity = 2
            };

            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.GetUserAsset(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(new UserAsset
                {
                    UserAssetId = Guid.NewGuid(),
                    Ticker = "PETR4",
                    TotalQuantity = 5,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-8),
                    CreatedByUserId = Guid.NewGuid()
                });
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.UpdateUserAsset(It.IsAny<UserAsset>()))
                .ReturnsAsync(HttpStatusCode.OK);

            //Act
            var result = await _service.RemoveToAssetUser(asset);

            //Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Remover todos os assets do usuário")]
        [Trait("Retirar Todos", "Success")]
        public async Task RemoveToUser_WithAQuantityEqualToTotalQuantity_DeactiveAndReturnSucess()
        {
            //Arrange
            Asset asset = new()
            {
                Ticker = "PETR4",
                UnitPrice = 5.50,
                Quantity = 5
            };

            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.GetUserAsset(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(new UserAsset
                {
                    UserAssetId = Guid.NewGuid(),
                    Ticker = "PETR4",
                    TotalQuantity = 5,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-8),
                    CreatedByUserId = Guid.NewGuid()
                });
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.UpdateUserAsset(It.IsAny<UserAsset>()))
                .ReturnsAsync(HttpStatusCode.OK);

            //Act
            var result = await _service.RemoveToAssetUser(asset);

            //Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Remover mais assets do que o usuário possui")]
        [Trait("Retirar Todos", "Fail")]
        public async Task RemoveToUser_WithAQuantityGreaterThanTotalQuantity_ReturnConflict()
        {
            //Arrange
            Asset asset = new()
            {
                Ticker = "PETR4",
                UnitPrice = 5.50,
                Quantity = 6
            };

            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.GetUserAsset(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(new UserAsset
                {
                    UserAssetId = Guid.NewGuid(),
                    Ticker = "PETR4",
                    TotalQuantity = 5,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-8),
                    CreatedByUserId = Guid.NewGuid()
                });
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.UpdateUserAsset(It.IsAny<UserAsset>()))
                .ReturnsAsync(HttpStatusCode.OK);

            //Act
            var result = await _service.RemoveToAssetUser(asset);

            //Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.Conflict, result);
        }

        [Fact(DisplayName = "Reverter uma compra")]
        [Trait("Reverter compra", "Success")]
        public async Task RevertAssetPurchase_WithAsset_ReturnSucess()
        {
            //Arrange
            Asset asset = new()
            {
                Quantity = 5,
                Ticker = "PETR4",
                UnitPrice = 5.50
            };
            UserAsset userAsset = new()
            {
                UserAssetId = Guid.NewGuid(),
                Ticker = "PETR4",
                IsActive = true,
                CreatedAt = DateTime.Now.AddDays(-5),
                CreatedByUserId = Guid.NewGuid(),
                TotalQuantity = 10,
            };

            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.GetUserAsset(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(userAsset);
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.UpdateUserAsset(It.IsAny<UserAsset>()))
                .ReturnsAsync(HttpStatusCode.OK);

            //Act
            var result = await _service.RevertAssetPurchase(asset);

            //Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }
    }
}
