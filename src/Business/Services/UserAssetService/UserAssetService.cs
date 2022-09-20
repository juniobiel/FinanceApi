using Business.Interfaces;
using Business.Models;
using System.Net;

namespace Business.Services.UserAssetService
{
    public class UserAssetService : IUserAssetService
    {
        readonly IUserAssetRepository _repository;
        readonly IUser _appUser;

        public UserAssetService( IUserAssetRepository userAssetRepository,
            IUser appUser )
        {
            _repository = userAssetRepository;
            _appUser = appUser;
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


        public async Task<UserAsset> SearchAsset( string ticker )
        {
            return await _repository.GetUserAsset(ticker, _appUser.GetUserId());
        }
        private async Task<HttpStatusCode> CreateUserAsset( Asset asset )
        {
            UserAsset userAsset = new()
            {
                Ticker = asset.Ticker,
                TotalQuantity = asset.Quantity,
                MediumPrice = asset.UnitPrice,
                CreatedAt = DateTime.Now,
                CreatedByUserId = _appUser.GetUserId()
            };

            return await _repository.CreateUserAsset(userAsset);
        }

        private async Task<HttpStatusCode> IncreaseUserAsset(UserAsset userAsset, Asset asset)
        {
            userAsset.TotalQuantity += asset.Quantity;
            userAsset.UpdatedAt = DateTime.Now;
            userAsset.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdateUserAsset(userAsset);
        }
    }
}
