using Business.Models;
using System.Net;

namespace Business.Services.UserAssetService
{
    public interface IUserAssetService
    {
        Task<HttpStatusCode> AddToUserAsset( Asset asset );
        Task<HttpStatusCode> RemoveToAssetUser( Asset asset );
        Task<UserAsset> SearchAsset( string ticker );
        Task<HttpStatusCode> RevertAssetPurchase(Asset asset );
        Task<HttpStatusCode> RevertAssetSell(Asset asset );
        Task<double> GetMediumPrice( UserAsset userAsset );
    }
}
