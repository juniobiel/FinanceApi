using Business.Interfaces;
using Business.Models;
using Business.Services.UserAssetService;
using System.Net;

namespace Business.Services.PurchaseService
{
    public class PurchaseService : IPurchaseService
    {
        readonly IUser _appUser;
        readonly IPurchaseRepository _repository;
        readonly IUserAssetService _userAssetService;

        public PurchaseService(IUser appUser, IPurchaseRepository repository, IUserAssetService userAssetService)
        {
            _appUser = appUser;
            _repository = repository;
            _userAssetService = userAssetService;
        }

        public async Task<HttpStatusCode> NewPurchase( Purchase purchase )
        {
            purchase.CreatedAt = DateTime.Now;
            purchase.CreatedByUserId = _appUser.GetUserId();

            foreach(Asset asset in purchase.Assets )
            {
                await _userAssetService.AddToUserAsset(asset);
            }

            return await _repository.AddPurchase(purchase);
        }
    }
}
