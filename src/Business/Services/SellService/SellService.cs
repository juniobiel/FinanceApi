using Business.Interfaces;
using Business.Models;
using Business.Services.UserAssetService;
using System.Net;

namespace Business.Services.SellService
{
    public class SellService : ISellService
    {
        readonly ISellRepository _repository;
        readonly IUserAssetService _userAssetService;
        readonly IUser _appUser;

        public SellService( ISellRepository sellRepository,
            IUser appUser,
            IUserAssetService userAssetService )
        {
            _repository = sellRepository;
            _appUser = appUser;
            _userAssetService = userAssetService;
        }


        public async Task<Sell> GetSell( Guid sellId )
        {
            return await _repository.GetSell(sellId, _appUser.GetUserId());
        }

        public async Task<HttpStatusCode> NewSell( Sell sell )
        {
            sell.CreatedAt = DateTime.Now;
            sell.CreatedByUserId = _appUser.GetUserId();

            foreach (Asset asset in sell.Assets)
                await _userAssetService.RemoveToAssetUser(asset);

            return await _repository.AddSell(sell);
        }

        public async Task<HttpStatusCode> UpdateSell( Sell sell )
        {
            if (!IsValidUser(sell.CreatedByUserId))
                return HttpStatusCode.Forbidden;

            sell.UpdatedAt = DateTime.Now;
            sell.UpdatedByUserId = _appUser.GetUserId();

            return await _repository.UpdateSell(sell);
        }

        public async Task<HttpStatusCode> DeleteSell( Sell sell )
        {
            if (!IsValidUser(sell.CreatedByUserId))
                return HttpStatusCode.Forbidden;

            foreach (Asset asset in sell.Assets)
                await _userAssetService.RevertAssetSell(asset);

            return await _repository.DeleteSell(sell);
        }

        private bool IsValidUser( Guid userId )
        {
            if (userId != _appUser.GetUserId())
                return false;
            return true;
        }
    }
}
