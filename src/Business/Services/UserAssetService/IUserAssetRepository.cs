using Business.Models;
using System.Net;

namespace Business.Services.UserAssetService
{
    public interface IUserAssetRepository
    {
        Task<HttpStatusCode> CreateUserAsset(UserAsset userAsset);
        Task<HttpStatusCode> UpdateUserAsset(UserAsset userAsset);
        Task<UserAsset> GetUserAsset( string ticker, Guid userId );
        Task<IEnumerable<Purchase>> GetPurchases(string ticker, Guid userId);
    }
}
