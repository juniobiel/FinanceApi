using Business.Models;
using Business.Services.UserAssetService;
using System.Net;

namespace Data.Repositories
{
    public class UserAssetRepository : IUserAssetRepository
    {
        public Task<HttpStatusCode> CreateUserAsset( UserAsset userAsset )
        {
            throw new NotImplementedException();
        }

        public Task<UserAsset> GetUserAsset( string ticker, Guid userId )
        {
            throw new NotImplementedException();
        }

        public Task<HttpStatusCode> UpdateUserAsset( UserAsset userAsset )
        {
            throw new NotImplementedException();
        }
    }
}
