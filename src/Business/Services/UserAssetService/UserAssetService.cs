using Business.Interfaces;
using Business.Models;
using Business.Services.PurchaseService;
using Business.Services.SellService;
using System.Net;

namespace Business.Services.UserAssetService
{
    public class UserAssetService : IUserAssetService
    {
        readonly IUserAssetRepository _repository;
        readonly IPurchaseRepository _purchaseRepository;
        readonly ISellRepository _sellRepository;
        readonly IUser _appUser;

        public UserAssetService( IUserAssetRepository userAssetRepository,
            IUser appUser,
            IPurchaseRepository purchaseRepository,
            ISellRepository sellRepository )
        {
            _repository = userAssetRepository;
            _appUser = appUser;
            _purchaseRepository = purchaseRepository;
            _sellRepository = sellRepository;
        }

        public async Task<HttpStatusCode> AddToUserAsset( Asset asset )
        {
            var userAsset = await SearchAsset(asset.Ticker);
            HttpStatusCode result;

            if (userAsset == null)
                result = await CreateUserAsset(asset);
            else
                result = await IncreaseUserAsset(userAsset, asset);

            return result;
        }

        public async Task<HttpStatusCode> RemoveToAssetUser( Asset asset )
        {
            var userAsset = await SearchAsset(asset.Ticker);

            if (userAsset == null)
                return HttpStatusCode.BadRequest;

            if (asset.Quantity > userAsset.TotalQuantity)
                return HttpStatusCode.Conflict;

            if (asset.Quantity == userAsset.TotalQuantity)
            {
                userAsset.TotalQuantity = 0;
                userAsset.IsActive = false;
            }
            else if (asset.Quantity < userAsset.TotalQuantity)
            {
                userAsset.TotalQuantity -= asset.Quantity;
            }

            userAsset.UpdatedAt = DateTime.Now;
            userAsset.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdateUserAsset(userAsset);
        }

        public async Task<HttpStatusCode> RevertAssetPurchase( Asset asset )
        {
            var userAsset = await SearchAsset(asset.Ticker);

            if (userAsset == null)
                return HttpStatusCode.BadRequest;

            userAsset.TotalQuantity -= asset.Quantity;
            if (userAsset.TotalQuantity <= 0)
            {
                userAsset.TotalQuantity = 0;
                userAsset.IsActive = false;
            }

            userAsset.UpdatedAt = DateTime.Now;
            userAsset.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdateUserAsset(userAsset);
        }

        public async Task<HttpStatusCode> RevertAssetSell( Asset asset )
        {
            var userAsset = await SearchAsset(asset.Ticker);

            if (userAsset == null)
                return HttpStatusCode.BadRequest;

            userAsset.TotalQuantity += asset.Quantity;
            userAsset.IsActive = true;
            userAsset.UpdatedAt = DateTime.Now;
            userAsset.UpdatedByUserId = _appUser.GetUserId();


            return await _repository.UpdateUserAsset(userAsset);
        }

        public async Task<UserAsset> SearchAsset( string ticker )
        {
            return await _repository.GetUserAsset(ticker, _appUser.GetUserId());
        }

        public async Task<HttpStatusCode> UpdateMediumPrice( string ticker )
        {
            var user = _appUser.GetUserId();
            var userAsset = await _repository.GetUserAsset(ticker, user);

            //TODO: Aplicar lógica para atualizar o LastMediumPrice
            userAsset.MediumPrice = await GetMediumPrice(userAsset);
            userAsset.UpdatedAt = DateTime.Now;
            userAsset.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdateUserAsset(userAsset);
        }

        public async Task<double> GetMediumPrice( UserAsset userAsset )
        {
            double mediumPrice = 0;

            if (!userAsset.IsActive)
                return 0;

            var result = await _purchaseRepository.GetPurchases(userAsset.Ticker, _appUser.GetUserId());
            if (userAsset.LastSell != null)
            {
                result = result.Where(x => x.PurchaseDate >= userAsset.LastSell).ToList();
                var sell = await _sellRepository.GetLastSell();
                mediumPrice = sell.MediumPrice;
            }

            foreach (var purchase in result)
            {
                if (purchase.Assets.Any())
                {
                    foreach (var asset in purchase.Assets.Where(x => x.Ticker == userAsset.Ticker))
                    {
                        mediumPrice += asset.TotalPaid + purchase.TotalTaxes;
                    }
                }
            }

            return mediumPrice /= userAsset.TotalQuantity;
        }

        private async Task<HttpStatusCode> CreateUserAsset( Asset asset )
        {

            UserAsset userAsset = new()
            {
                Ticker = asset.Ticker,
                TotalQuantity = asset.Quantity,
                IsActive = true,
                CreatedAt = DateTime.Now,
                CreatedByUserId = _appUser.GetUserId(),
            };

            return await _repository.CreateUserAsset(userAsset);
        }

        private async Task<HttpStatusCode> IncreaseUserAsset( UserAsset userAsset, Asset asset )
        {
            userAsset.IsActive = true;
            userAsset.TotalQuantity += asset.Quantity;
            userAsset.UpdatedAt = DateTime.Now;
            userAsset.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdateUserAsset(userAsset);
        }
    }
}
