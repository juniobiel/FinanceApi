using Business.Models;
using System.Net;

namespace Business.Services.PurchaseService
{
    public interface IPurchaseService
    {
        Task<Purchase> GetPurchase( Guid purchaseId );
        Task<HttpStatusCode> NewPurchase( Purchase purchase );
        Task<HttpStatusCode> UpdatePurchase( Purchase purchase );
        Task<HttpStatusCode> DeletePurchase( Purchase purchase );
    }
}
