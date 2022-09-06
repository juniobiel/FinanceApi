using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IUserAssetRepository
    {
        Task<UserAsset> CreateUserAsset( UserAsset userAsset );
        Task UpdateUserAsset( UserAsset userAsset );
        Task<int> CreateUserPurchase( AssetPurchase purchase );
        Task<UserAsset> GetUserAsset( string ticker, Guid userId );
        Task<IEnumerable<AssetPurchase>> GetPurchases( string ticker, Guid userId );
    }
}
