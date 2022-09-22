using Business.Interfaces;
using Business.Models;
using Business.Services.SellService;
using Moq;
using Moq.AutoMock;
using System.Net;

namespace UnitTests.Services
{
    public class SellServiceTests
    {
        readonly AutoMocker _mocker;
        readonly ISellService _service;

        public SellServiceTests()
        {
            _mocker = new AutoMocker();
            _service = _mocker.CreateInstance<SellService>();
        }

        [Fact(DisplayName = "Registrar uma Venda de Asset")]
        [Trait("Registrar", "Success")]
        public async Task NewSell_SellWithAssets_ReturnSucess()
        {
            //Arrange
            Sell sell = new()
            {
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Quantity = 5,
                        UnitPrice = 5,
                        Ticker = "PETR4"
                    }
                },
                TotalTaxes = 0,
                SellDate = DateTime.Now.AddDays(-8)               
            };

            _mocker.GetMock<ISellRepository>()
                .Setup(x => x.AddSell(It.IsAny<Sell>()))
                .ReturnsAsync(HttpStatusCode.OK);

            //Act
            var result = await _service.NewSell(sell);

            //Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Editar uma Venda de Asset")]
        [Trait("Editar", "Success")]
        public async Task UpdateSell_SellWithAssets_ReturnSucess()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Sell sell = new()
            {
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Quantity = 5,
                        UnitPrice = 5,
                        Ticker = "PETR4"
                    }
                },
                TotalTaxes = 0,
                SellDate = DateTime.Now.AddDays(-8),
                CreatedByUserId = userId
            };

            _mocker.GetMock<IUser>()
                .Setup(x => x.GetUserId())
                .Returns(userId);
            _mocker.GetMock<ISellRepository>()
                .Setup(x => x.UpdateSell(It.IsAny<Sell>()))
                .ReturnsAsync(HttpStatusCode.OK);

            //Act
            var result = await _service.UpdateSell(sell);

            //Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Editar uma venda com usuário diferente")]
        [Trait("Editar", "Fail")]
        public async Task UpdateSell_UserNotAuthorized_ReturnForbidden()
        {
            // Arrange
            Sell sell = new()
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
            var result = await _service.UpdateSell(sell);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.Forbidden, result);
        }

        [Fact(DisplayName = "Deletar uma Venda de Asset")]
        [Trait("Deletar", "Success")]
        public async Task DeleteSell_SellWithAssets_ReturnSucess()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Sell sell = new()
            {
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Quantity = 5,
                        UnitPrice = 5,
                        Ticker = "PETR4"
                    }
                },
                TotalTaxes = 0,
                SellDate = DateTime.Now.AddDays(-8),
                CreatedByUserId = userId
            };

            _mocker.GetMock<IUser>()
                .Setup(x => x.GetUserId())
                .Returns(userId);
            _mocker.GetMock<ISellRepository>()
                .Setup(x => x.DeleteSell(It.IsAny<Sell>()))
                .ReturnsAsync(HttpStatusCode.OK);

            //Act
            var result = await _service.DeleteSell(sell);

            //Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        [Fact(DisplayName = "Deletar uma venda com usuário diferente")]
        [Trait("Deletar", "Fail")]
        public async Task DeletePurchase_UserNotAuthorized_ReturnForbidden()
        {
            // Arrange
            Sell sell = new()
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
            var result = await _service.DeleteSell(sell);

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.Forbidden, result);
        }

        [Fact(DisplayName = "Ler uma Venda de Asset")]
        [Trait("Ler", "Success")]
        public async Task GetSell_PassingSellId_ReturnSell()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Sell sell = new()
            {
                Id = Guid.NewGuid(),
                Assets = new List<Asset>
                {
                    new Asset
                    {
                        Quantity = 5,
                        UnitPrice = 5,
                        Ticker = "PETR4"
                    }
                },
                TotalTaxes = 0,
                SellDate = DateTime.Now.AddDays(-8),
                CreatedByUserId = userId
            };

            _mocker.GetMock<IUser>()
                .Setup(x => x.GetUserId())
                .Returns(userId);
            _mocker.GetMock<ISellRepository>()
                .Setup(x => x.GetSell(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(sell);

            //Act
            var result = await _service.GetSell(sell.Id);

            //Assert
            Assert.IsType<Sell>(result);
            Assert.Equal(sell, result);
        }

    }
}
