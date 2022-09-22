using Business.Models;
using System.Net;

namespace Business.Services.SellService
{
    public interface ISellService
    {
        Task<HttpStatusCode> NewSell( Sell sell );
        Task<HttpStatusCode> UpdateSell( Sell sell );
        Task<HttpStatusCode> DeleteSell( Sell sell );
        Task<Sell> GetSell( Guid sellId );
    }
}
