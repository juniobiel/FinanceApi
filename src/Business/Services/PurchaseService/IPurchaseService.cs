using Business.Models;
using System.Net;

namespace Business.Services.PurchaseService
{
    public interface IPurchaseService
    {
        Task<HttpStatusCode> NewPurchase( Purchase purchase );
    }
}
