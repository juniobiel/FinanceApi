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

        [Fact(DisplayName = "Criar um asset que não existe na base de dados e tipar para FII")]
        [Trait("create-asset", "Status 200")]
        public async void CreateAsset_WhenAssetDoesNotExistsInDataBaseTypeFII_ReturnCode200()
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
            Asset asset = new()
            {
                Id = Guid.NewGuid(),
                Ticker = "BRCO11",
                AssetType = AssetType.FII,
                CurrentPrice = 0
            };
            AlphaVantageAssetHistory assetHistory = new()
            {
                MetaData = new(),
                TimeSeries = new ()
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
                .Setup(x => x.CreateNewAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));
            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.GetAssetHistory(It.IsAny<string>()))
                .Returns(Task.FromResult(assetHistory));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));

            //Act
            var result = await _service.CreateAsset("BRCO11");

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(200, result);
        }

        [Fact(DisplayName = "Criar um asset que não existe na base de dados e tipar para ETF")]
        [Trait("create-asset", "Status 200")]
        public async void CreateAsset_WhenAssetDoesNotExistsInDataBaseTypeETF_ReturnCode200()
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
            Asset asset = new()
            {
                Id = Guid.NewGuid(),
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
                .Setup(x => x.CreateNewAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));
            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.GetAssetHistory(It.IsAny<string>()))
                .Returns(Task.FromResult(assetHistory));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));

            //Act
            var result = await _service.CreateAsset("BOVA11");

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(200, result);
        }

        [Fact(DisplayName = "Criar um asset que não existe na base de dados e tipar para ETF")]
        [Trait("create-asset", "Status 200")]
        public async void CreateAsset_WhenAssetDoesNotExistsInDataBaseTypeAcao_ReturnCode200()
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
            Asset asset = new()
            {
                Id = Guid.NewGuid(),
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
                .Setup(x => x.CreateNewAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));
            _mocker.GetMock<IAlphaVantageService>()
                .Setup(x => x.GetAssetHistory(It.IsAny<string>()))
                .Returns(Task.FromResult(assetHistory));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.GetAsset(It.IsAny<string>()))
                .Returns(Task.FromResult(asset));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));

            //Act
            var result = await _service.CreateAsset("PETR4");

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(200, result);
        }

        [Fact(DisplayName = "Criar um asset que não existe na bolsa de valores e retornar status 400")]
        [Trait("create-asset", "Status 400")]
        public async void CreateAsset_WhenAssetDoesNotExistsOfficially_ReturnCode400()
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
            Assert.IsType<int>(result);
            Assert.Equal(400, result);
        }

        [Fact(DisplayName = "Houve erro ao criar o asset no banco de dados, deve retornar status 400")]
        [Trait("create-asset", "Status 400")]
        public async void CreateAsset_WhenCannotCreateAtDataBase_ReturnCode400()
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
                .Setup(x => x.CreateNewAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(400));

            //Act
            var result = await _service.CreateAsset("BRCO11");

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(400, result);
        }

        [Fact(DisplayName = "Houve erro ao atualizar o asset no banco de dados, deve retornar status 400")]
        [Trait("create-asset", "Status 400")]
        public async void CreateAsset_WhenCannotUpdateAtDataBase_ReturnCode400()
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
            Asset asset = new()
            {
                Id = Guid.NewGuid(),
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
                .Setup(x => x.CreateNewAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<Asset>()))
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
            Assert.IsType<int>(result);
            Assert.Equal(400, result);
        }

        [Fact(DisplayName = "O dia atual ainda não consta na consulta do AlphaService")]
        [Trait("create-asset", "Status 200")]
        public async void CreateAsset_WhenAlphaServiceHasNotUpdatedLastDay_ReturnCode200()
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
            Asset asset = new()
            {
                Id = Guid.NewGuid(),
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
                .Setup(x => x.CreateNewAsset(It.IsAny<Asset>()))
                .Returns(Task.FromResult(200));
            _mocker.GetMock<IAssetRepository>()
                .Setup(x => x.UpdateAsset(It.IsAny<Asset>()))
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
            Assert.IsType<int>(result);
            Assert.Equal(200, result);
        }
    }
}
