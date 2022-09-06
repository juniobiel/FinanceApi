using Business.Interfaces.Repositories;
using Business.Models;
using Business.Models.Enums;
using Business.Services.AssetService;
using Business.Services.UserAssetService;
using Moq;
using Moq.AutoMock;

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

        #region AssetPurchase

        [Fact(DisplayName = "Registrar uma compra de FII e retornar sucesso")]
        [Trait("create new purchase", "sucess 200")]
        public async void CreateNewAssetPurchase_WhenAssetIsFIIAndNotExists_ReturnSuccess200()
        {
            //Arrange
            AssetPurchase purchase = new()
            {
                PurchaseId = Guid.NewGuid(),
                Ticker = "BRCO11",
                Quantity = 5,
                UnitPrice = 10,
                BrokerTax = 0,
                IncomeTax = 0.05,
                OtherTaxes = 0.3,
                CreatedByUserId = Guid.NewGuid()
            };
            Asset asset = new()
            {
                AssetId = Guid.NewGuid(),
                AssetType = AssetType.FII,
                Ticker = "BRCO11",
                CurrentPrice = 12.5,
                UpdatedAt = DateTime.Now.AddDays(-2),
                UpdatedByUser = Guid.NewGuid()
            };
            UserAsset userAsset = new()
            {
                UserAssetId = Guid.NewGuid(),
                AssetId = asset.AssetId,
                Ticker = asset.Ticker,
                TotalQuantity = 0,
                MediumPrice = 0,
                CreatedByUserId = purchase.CreatedByUserId,
                CreatedAt = DateTime.Now
            };
            List<AssetPurchase> purchases = new()
            {
                new AssetPurchase
                {
                    PurchaseId = Guid.NewGuid(),
                    Ticker = "BRCO11",
                    Quantity = 5,
                    UnitPrice = 10,
                    BrokerTax = 0,
                    IncomeTax = 0.05,
                    OtherTaxes = 0.3,
                    CreatedByUserId = Guid.NewGuid()
                },
                new AssetPurchase
                {
                    PurchaseId = Guid.NewGuid(),
                    Ticker = "BRCO11",
                    Quantity = 5,
                    UnitPrice = 10,
                    BrokerTax = 0,
                    IncomeTax = 0.05,
                    OtherTaxes = 0.3,
                    CreatedByUserId = Guid.NewGuid()
                }
            };

            _mocker.GetMock<IAssetService>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult((Asset)null));
            _mocker.GetMock<IAssetService>()
                .Setup(x => x.CreateAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.GetUserAsset(It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult((UserAsset)null));
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.CreateUserAsset(It.IsAny<UserAsset>()))
                .Returns(Task.FromResult(userAsset));
            _mocker.GetMock<IUserAssetRepository>()
                .Setup(x => x.GetPurchases(It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult((IEnumerable<AssetPurchase>) purchases));

            //Act
            var result = await _service.NewPurchase(purchase);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(200, result);
        }

        [Fact(DisplayName = "Registrar uma compra de FII e retornar sucesso")]
        [Trait("create new purchase", "sucess 200")]
        public async void CreateNewAssetPurchase_WhenAssetIsFIIExists_ReturnSuccess200()
        {
            //Arrange


            //Act


            //Assert
        }

        #endregion
    }
}
