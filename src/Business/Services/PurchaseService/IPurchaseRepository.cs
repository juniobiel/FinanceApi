using Business.Models;
using System.Net;

namespace Business.Services.PurchaseService
{
    public interface IPurchaseRepository
    {
        Task<HttpStatusCode> AddPurchase( Purchase purchase );
    }
}
