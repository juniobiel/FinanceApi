using Business.Models;
using Business.Services.PurchaseService;
using Business.Services.UserAssetService;
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
                PurchaseId = Guid.NewGuid(),
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
                PurchaseId = Guid.NewGuid(),
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
    }
}
