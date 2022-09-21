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

        public async Task<Purchase> GetPurchase(Guid purchaseId) => await _repository.GetPurchase( purchaseId, _appUser.GetUserId());

        public async Task<HttpStatusCode> UpdatePurchase(Purchase purchase)
        {
            if(!IsValidUser(purchase.CreatedByUserId))
                return HttpStatusCode.Forbidden;
            
            purchase.UpdatedAt = DateTime.Now;
            purchase.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdatePurchase(purchase);
        }

        public async Task<HttpStatusCode> DeletePurchase(Purchase purchase)
        {
            if (!IsValidUser(purchase.CreatedByUserId))
                return HttpStatusCode.Forbidden;

            return await _repository.DeletePurchase(purchase);
        }

        private bool IsValidUser(Guid userId)
        {
            if (userId != _appUser.GetUserId())
                return false;
            return true;
        }
    }
}
