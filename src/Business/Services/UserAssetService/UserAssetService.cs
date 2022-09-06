using Business.Interfaces;
using Business.Interfaces.Repositories;
using Business.Models;
using Business.Services.AssetService;
using Microsoft.AspNetCore.Http;

namespace Business.Services.UserAssetService
{
    public class UserAssetService : IUserAssetService
    {
        readonly IUserAssetRepository _userAssetRepository;
        readonly IAssetService _assetService;
        readonly IUser _appUser;
        
        public UserAssetService( IUserAssetRepository userAssetRepository,
            IAssetService assetService,
            IUser appUser)
        {
            _userAssetRepository = userAssetRepository;
            _assetService = assetService;
            _appUser = appUser;
        }

        public async Task<int> NewPurchase( AssetPurchase purchase )
        {
            Asset asset = await _assetService.GetAsset(purchase.Ticker);
            asset ??= await _assetService.CreateAsset(purchase.Ticker);

            var userAsset = await GetUserAsset(purchase.Ticker);
            userAsset ??= await CreateUserAsset(purchase.Ticker, asset.AssetId);

            purchase.UserAssetId = userAsset.UserAssetId;
            purchase.CreatedByUserId = _appUser.GetUserId();
            purchase.CreatedAt = DateTime.Now;
            
            if(purchase.TotalPaid == 0)
                purchase.TotalPaid = (purchase.UnitPrice * purchase.Quantity) + (purchase.BrokerTax + purchase.IncomeTax + purchase.OtherTaxes);

            await _userAssetRepository.CreateUserPurchase(purchase);

            userAsset.AssetId = asset.AssetId;
            userAsset.CreatedAt = DateTime.Now;
            userAsset.CreatedByUserId = _appUser.GetUserId();
            userAsset.AssetId = asset.AssetId;
            userAsset.TotalQuantity += purchase.Quantity;

            userAsset = await UpdateMediumPrice(userAsset);
            await _userAssetRepository.UpdateUserAsset(userAsset);

            return StatusCodes.Status200OK;
        }

        public async Task<UserAsset> CreateUserAsset( string ticker, Guid assetId )
        {
            UserAsset userAsset = new()
            {
                AssetId = assetId,
                CreatedAt = DateTime.Now,
                CreatedByUserId = _appUser.GetUserId(),
                MediumPrice = 0,
                TotalQuantity = 0
            };

            return await _userAssetRepository.CreateUserAsset(userAsset);
        }

        public async Task<UserAsset> GetUserAsset( string ticker )
        {
            return await _userAssetRepository.GetUserAsset(ticker, _appUser.GetUserId());
        }

        public async Task<UserAsset> UpdateMediumPrice(UserAsset userAsset)
        {
            var purchasesResult = await _userAssetRepository.GetPurchases(userAsset.Ticker, _appUser.GetUserId());
            double mediumPrice = 0;
            foreach(var purchase in purchasesResult)
            {
                mediumPrice += purchase.TotalPaid;
            }
            userAsset.MediumPrice = mediumPrice / userAsset.TotalQuantity;

            return userAsset;
        }
    }
}
