using Business.Interfaces;
using Business.Models;
using System.Net;

namespace Business.Services.UserAssetService
{
    public class UserAssetService : IUserAssetService
    {
        readonly IUserAssetRepository _repository;
        readonly IUser _appUser;
        //readonly IAssetPriceService _priceService;

        public UserAssetService( IUserAssetRepository userAssetRepository,
            IUser appUser)
            //IAssetPriceService priceService )
        {
            _repository = userAssetRepository;
            _appUser = appUser;
            //_priceService = priceService;
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

            if(userAsset == null)
                return HttpStatusCode.BadRequest;

            if(asset.Quantity > userAsset.TotalQuantity)
                return HttpStatusCode.Conflict;

            if(asset.Quantity == userAsset.TotalQuantity)
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
            if(userAsset.TotalQuantity <= 0)
            {
                userAsset.TotalQuantity = 0;
                userAsset.IsActive = false;
            }

            userAsset.UpdatedAt = DateTime.Now;
            userAsset.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdateUserAsset(userAsset);
        }

        public Task<HttpStatusCode> RevertAssetSell( Asset asset )
        {
            throw new NotImplementedException();
        }

        public async Task<UserAsset> SearchAsset( string ticker )
        {
            return await _repository.GetUserAsset(ticker, _appUser.GetUserId());
        }

        private async Task<HttpStatusCode> CreateUserAsset( Asset asset )
        {
            //var assetPrice = await _priceService.GetOrCreateAssetPrice(asset.Ticker);

            UserAsset userAsset = new()
            {
                Ticker = asset.Ticker,
                TotalQuantity = asset.Quantity,
                IsActive = true,
                CreatedAt = DateTime.Now,
                CreatedByUserId = _appUser.GetUserId()
            };

            return await _repository.CreateUserAsset(userAsset);
        }

        private async Task<HttpStatusCode> IncreaseUserAsset(UserAsset userAsset, Asset asset)
        {
            userAsset.IsActive = true;
            userAsset.TotalQuantity += asset.Quantity;
            userAsset.UpdatedAt = DateTime.Now;
            userAsset.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdateUserAsset(userAsset);
        }
    }
}
