using Business.Models;
using System.Net;

namespace Business.Services.UserAssetService
{
    public interface IUserAssetService
    {
        Task<HttpStatusCode> AddToUserAsset( Asset asset );
        Task<UserAsset> SearchAsset( string ticker );
    }
}
