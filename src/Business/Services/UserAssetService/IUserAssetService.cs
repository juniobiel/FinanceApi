using Business.Models;

namespace Business.Services.UserAssetService
{
    public interface IUserAssetService
    {
        Task<UserAsset> CreateUserAsset( string ticker, Guid assetId );
        Task<UserAsset> GetUserAsset( string ticker );
        Task<UserAsset> UpdateMediumPrice( UserAsset userAsset );
    }
}
