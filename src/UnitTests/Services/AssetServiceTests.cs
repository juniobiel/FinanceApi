using Business.Models;
using Business.Models.Enums;
using Business.Services.AlphaVantage;
using Business.Services.AlphaVantage.ViewModels;
using Business.Services.AssetService;
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

        #region AssetCreate
        [Fact(DisplayName = "Criar um asset que não existe na base de dados e tipar para FII")]
        [Trait("create-asset", "Success Return Asset")]
        public async void CreateAsset_WhenAssetDoesNotExistsInDataBaseTypeFII_ReturnAsset()
        {
            //Arrange
            AlphaVantageSearchResult searchResult = new()
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
            AssetPrice asset = new()
            {
                AssetId = Guid.NewGuid(),
                Ticker = "BRCO11",
                AssetType = AssetType.FII,
                CurrentPrice = 0
            };
            AlphaVantageAssetHistory assetHistory = new()
            {
                MetaData = new(),
                TimeSeries = new()
            };
            assetHistory.TimeSeries.Add(DateTime.Now.Date.ToString(), new DayReport()
            {
                Open = "10.00",
                High = "15.00",
                Low = "9.00",
                Close = "12.50",
                Volume = "152000"
            });

            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.SearchAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(searchResult));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.CreateNewAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.GetAssetHistory(It.IsAny<string>()))
                .Returns(Task.FromResult(assetHistory));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(200));

            //Act
            var result = await _service.CreateAsset("BRCO11");

            //Assert
            Assert.IsType<AssetPrice>(result);
        }

        [Fact(DisplayName = "Criar um asset que não existe na base de dados e tipar para ETF")]
        [Trait("create-asset", "Success Return Asset")]
        public async void CreateAsset_WhenAssetDoesNotExistsInDataBaseTypeETF_ReturnAsset()
        {
            //Arrange
            AlphaVantageSearchResult searchResult = new()
            {
                BestMatches = new List<AlphaVantageAssetInformation>
                {
                    new AlphaVantageAssetInformation
                    {
                        symbol = "BOVA11.SAO",
                        name = "iShares Ibovespa Index Fund",
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
            AssetPrice asset = new()
            {
                AssetId = Guid.NewGuid(),
                Ticker = "BOVA11",
                AssetType = AssetType.ETF,
                CurrentPrice = 0
            };
            AlphaVantageAssetHistory assetHistory = new()
            {
                MetaData = new(),
                TimeSeries = new()
            };
            assetHistory.TimeSeries.Add(DateTime.Now.Date.ToString(), new DayReport()
            {
                Open = "10.00",
                High = "15.00",
                Low = "9.00",
                Close = "12.50",
                Volume = "152000"
            });

            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.SearchAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(searchResult));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.CreateNewAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.GetAssetHistory(It.IsAny<string>()))
                .Returns(Task.FromResult(assetHistory));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(200));

            //Act
            var result = await _service.CreateAsset("BOVA11");

            //Assert
            Assert.IsType<AssetPrice>(result);
        }

        [Fact(DisplayName = "Criar um asset que não existe na base de dados e tipar para ETF")]
        [Trait("create-asset", "Success Return Asset")]
        public async void CreateAsset_WhenAssetDoesNotExistsInDataBaseTypeAcao_ReturnAsset()
        {
            //Arrange
            AlphaVantageSearchResult searchResult = new()
            {
                BestMatches = new List<AlphaVantageAssetInformation>
                {
                    new AlphaVantageAssetInformation
                    {
                        symbol = "PETR4.SAO",
                        name = "Petróleo Brasileiro S.A. - Petrobras",
                        type = "Equity",
                        region = "Brazil/Sao Paolo",
                        marketOpen = "10:00",
                        marketClose = "17:30",
                        timezone = "UTC-03",
                        currency = "BRL",
                        matchScore =  "1.0000"
                    }
                }
            };
            AssetPrice asset = new()
            {
                AssetId = Guid.NewGuid(),
                Ticker = "PETR4",
                AssetType = AssetType.Acao,
                CurrentPrice = 0
            };
            AlphaVantageAssetHistory assetHistory = new()
            {
                MetaData = new(),
                TimeSeries = new()
            };
            assetHistory.TimeSeries.Add(DateTime.Now.Date.ToString(), new DayReport()
            {
                Open = "10.00",
                High = "15.00",
                Low = "9.00",
                Close = "12.50",
                Volume = "152000"
            });

            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.SearchAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(searchResult));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.CreateNewAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.GetAssetHistory(It.IsAny<string>()))
                .Returns(Task.FromResult(assetHistory));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(200));

            //Act
            var result = await _service.CreateAsset("PETR4");

            //Assert
            Assert.IsType<AssetPrice>(result);
        }

        [Fact(DisplayName = "Criar um asset que não existe na bolsa de valores e retornar null")]
        [Trait("create-asset", "Return Null")]
        public async void CreateAsset_WhenAssetDoesNotExistsOfficially_ReturnNull()
        {
            //Arrange
            AlphaVantageSearchResult searchResult = new()
            {
                BestMatches = new List<AlphaVantageAssetInformation>()
            };

            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.SearchAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(searchResult));

            //Act
            var result = await _service.CreateAsset("BRCO1");

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Houve erro ao criar o asset no banco de dados, deve retornar null")]
        [Trait("create-asset", "Return Null")]
        public async void CreateAsset_WhenCannotCreateAtDataBase_ReturnNull()
        {
            //Arrange
            AlphaVantageSearchResult searchResult = new()
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
                .Setup(x => x.CreateNewAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult((AssetPrice) null));

            //Act
            var result = await _service.CreateAsset("BRCO11");

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Houve erro ao atualizar o asset no banco de dados, deve retornar null")]
        [Trait("create-asset", "Return Null")]
        public async void CreateAsset_WhenCannotUpdateAtDataBase_ReturnNull()
        {
            //Arrange
            AlphaVantageSearchResult searchResult = new()
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
            AssetPrice asset = new()
            {
                AssetId = Guid.NewGuid(),
                Ticker = "BRCO11",
                AssetType = AssetType.FII,
                CurrentPrice = 0
            };
            AlphaVantageAssetHistory assetHistory = new()
            {
                MetaData = new(),
                TimeSeries = new()
            };
            assetHistory.TimeSeries.Add(DateTime.Now.Date.ToString(), new DayReport()
            {
                Open = "10.00",
                High = "15.00",
                Low = "9.00",
                Close = "12.50",
                Volume = "152000"
            });

            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.SearchAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(searchResult));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.CreateNewAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(400));
            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.GetAssetHistory(It.IsAny<string>()))
                .Returns(Task.FromResult(assetHistory));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));

            //Act
            var result = await _service.CreateAsset("BRCO11");

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "O dia atual ainda não consta na consulta do AlphaService")]
        [Trait("create-asset", "Success Return Asset")]
        public async void CreateAsset_WhenAlphaServiceHasNotUpdatedLastDay_ReturnAsset()
        {
            //Arrange
            AlphaVantageSearchResult searchResult = new()
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
            AssetPrice asset = new()
            {
                AssetId = Guid.NewGuid(),
                Ticker = "BRCO11",
                AssetType = AssetType.FII,
                CurrentPrice = 0
            };
            AlphaVantageAssetHistory assetHistory = new()
            {
                MetaData = new(),
                TimeSeries = new()
            };
            assetHistory.TimeSeries.Add(DateTime.Now.AddDays(-1).Date.ToString(), new DayReport()
            {
                Open = "10.00",
                High = "15.00",
                Low = "9.00",
                Close = "12.50",
                Volume = "152000"
            });

            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.SearchAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(searchResult));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.CreateNewAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<AssetPrice>()))
                .Returns(Task.FromResult(200));
            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.GetAssetHistory(It.IsAny<string>()))
                .Returns(Task.FromResult(assetHistory));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));

            //Act
            var result = await _service.CreateAsset("BRCO11");

            //Assert
            Assert.IsType<AssetPrice>(result);

        }

        #endregion

    }
}
