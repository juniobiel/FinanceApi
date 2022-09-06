using Business.Interfaces.Repositories;
using Business.Models;

namespace Data.Repositories
{
    public class UserAssetRepository : IUserAssetRepository
    {
        public Task<UserAsset> CreateUserAsset( UserAsset userAsset )
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateUserPurchase( AssetPurchase purchase )
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetPurchase>> GetPurchases( string ticker, Guid userId )
        {
            throw new NotImplementedException();
        }

        public Task<UserAsset> GetUserAsset( string ticker, Guid userId )
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsset( UserAsset userAsset )
        {
            throw new NotImplementedException();
        }
    }
}
