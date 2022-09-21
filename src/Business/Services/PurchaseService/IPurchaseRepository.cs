using Business.Models;
using System.Net;

namespace Business.Services.PurchaseService
{
    public interface IPurchaseRepository
    {
        Task<Purchase> GetPurchase( Guid purchaseId, Guid userId );
        Task<HttpStatusCode> AddPurchase( Purchase purchase );
        Task<HttpStatusCode> UpdatePurchase( Purchase purchase );
        Task<HttpStatusCode> DeletePurchase( Purchase purchase );
    }
}
