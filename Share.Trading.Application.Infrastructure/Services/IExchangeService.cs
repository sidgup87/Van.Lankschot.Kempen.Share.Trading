using Share.Trading.Domain.Entities.Models;

namespace Share.Trading.Application.Infrastructure.Services
{
    public interface IExchangeService
    {
        Task<SharesDetails> GetShareAsync(string symbol, CancellationToken cancellationToken);

        Task<List<SharesDetails>> GetAllSharesAsync(CancellationToken cancellationToken);
    }
}
