using Business.Models;
using System.Net;

namespace Business.Services.SellService
{
    public interface ISellRepository
    {
        Task<HttpStatusCode> AddSell( Sell sell );
        Task<HttpStatusCode> UpdateSell( Sell sell );
        Task<HttpStatusCode> DeleteSell( Sell sell );
        Task<Sell> GetSell( Guid sellId, Guid userId );
    }
}
