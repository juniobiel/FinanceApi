using Business.Interfaces;
using Business.Models;
using Business.Services.PurchaseService;
using Moq;
using Moq.AutoMock;
using System.Net;

namespace UnitTests.Services
{
    public class PurchaseServiceTests
    {
        readonly AutoMocker _mocker;
        readonly IPurchaseService _service;

        public PurchaseServiceTests()
        {
            _mocker = new AutoMocker();
            _service = _mocker.CreateInstance<PurchaseService>();
        }

        [Fact(DisplayName = "Registrar uma compra com lista de assets e taxas pagas quando o usuário não possui o Asset")]
        [Trait("Registrar", "Success")]
        public async Task NewPurchase_WhenAssetDoesNotExistsToUser_ReturnSucess()
        {
            // Arrange
            Purchase purchase = new()
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = Guid.NewGuid(),
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Ticker = "VGHF11",
                        Quantity = 2,
                        UnitPrice = 9.86
                    }
                },
                TotalTaxes = 0
            };

            _mocker.GetMock<IPurchaseRepository>()
                .Setup(x => x.AddPurchase(It.IsAny<Purchase>()))
                .ReturnsAsync(HttpStatusCode.OK);

            // Act
            var result = await _service.NewPurchase(purchase);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Registrar uma compra com lista de assets e taxas pagas quando o usuário possui o Asset")]
        [Trait("Registrar", "Success")]
        public async Task NewPurchase_WhenAssetExistsToUser_ReturnSucess()
        {
            // Arrange
            Purchase purchase = new()
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = Guid.NewGuid(),
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Ticker = "VGHF11",
                        Quantity = 2,
                        UnitPrice = 9.86
                    }
                },
                TotalTaxes = 0
            };

            _mocker.GetMock<IPurchaseRepository>()
                .Setup(x => x.AddPurchase(It.IsAny<Purchase>()))
                .ReturnsAsync(HttpStatusCode.OK);

            // Act
            var result = await _service.NewPurchase(purchase);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Editar uma compra")]
        [Trait("Editar", "Success")]
        public async Task UpdatePurchase_PassingNewInformation_ReturnSucess()
        {
            // Arrange
            Purchase purchase = new()
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = Guid.NewGuid(),
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Ticker = "VGHF11",
                        Quantity = 2,
                        UnitPrice = 9.86
                    }
                },
                TotalTaxes = 0
            };

            _mocker.GetMock<IPurchaseRepository>()
                .Setup(x => x.UpdatePurchase(It.IsAny<Purchase>()))
                .ReturnsAsync(HttpStatusCode.OK);
            _mocker.GetMock<IUser>()
                .Setup(x => x.GetUserId())
                .Returns(purchase.CreatedByUserId);

            // Act
            var result = await _service.UpdatePurchase(purchase);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Editar uma compra com usuário diferente")]
        [Trait("Editar", "Fail")]
        public async Task UpdatePurchase_UserNotAuthorized_ReturnForbidden()
        {
            // Arrange
            Purchase purchase = new()
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = Guid.NewGuid(),
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Ticker = "VGHF11",
                        Quantity = 2,
                        UnitPrice = 9.86
                    }
                },
                TotalTaxes = 0
            };

            _mocker.GetMock<IUser>()
                .Setup(x => x.GetUserId())
                .Returns(Guid.NewGuid());

            // Act
            var result = await _service.UpdatePurchase(purchase);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.Forbidden, result);
        }

        [Fact(DisplayName = "Deletar uma compra")]
        [Trait("Deletar", "Success")]
        public async Task DeletePurchase_PassingPurchase_ReturnSucess()
        {
            // Arrange
            Purchase purchase = new()
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = Guid.NewGuid(),
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Ticker = "VGHF11",
                        Quantity = 2,
                        UnitPrice = 9.86
                    }
                },
                TotalTaxes = 0
            };

            _mocker.GetMock<IPurchaseRepository>()
                .Setup(x => x.DeletePurchase(It.IsAny<Purchase>()))
                .ReturnsAsync(HttpStatusCode.OK);
            _mocker.GetMock<IUser>()
                .Setup(x => x.GetUserId())
                .Returns(purchase.CreatedByUserId);

            // Act
            var result = await _service.DeletePurchase(purchase);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Deletar uma compra com usuário diferente")]
        [Trait("Deletar", "Fail")]
        public async Task DeletePurchase_UserNotAuthorized_ReturnForbidden()
        {
            // Arrange
            Purchase purchase = new()
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = Guid.NewGuid(),
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Ticker = "VGHF11",
                        Quantity = 2,
                        UnitPrice = 9.86
                    }
                },
                TotalTaxes = 0
            };

            _mocker.GetMock<IUser>()
                .Setup(x => x.GetUserId())
                .Returns(Guid.NewGuid());

            // Act
            var result = await _service.DeletePurchase(purchase);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.Forbidden, result);
        }

        [Fact(DisplayName = "Ler uma compra")]
        [Trait("Ler", "Success")]
        public async Task GetPurchase_PassingPurchaseId_ReturnPurchase()
        {
            //Arrange
            Guid purchaseId = Guid.NewGuid();

            _mocker.GetMock<IPurchaseRepository>()
                .Setup(x => x.GetPurchase(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Purchase
                {
                    Id = purchaseId,
                    CreatedByUserId = Guid.NewGuid(),
                    Assets = new List<Asset>
                    {
                        new Asset
                        {
                            Ticker = "VGHF11",
                            Quantity = 2,
                            UnitPrice = 9.86
                        }
                    },
                    TotalTaxes = 0
                });

            //Act
            var result = await _service.GetPurchase(purchaseId);

            //Assert
            Assert.IsType<Purchase>(result);
            Assert.Equal(purchaseId, result.Id);
        }
    }
}
