using Share.Trading.Domain.Entities.Models;

namespace Share.Trading.Application.Infrastructure.Services
{
    public interface IPortfolioService
    {
        Task<Portfolio> GetPortfolioAsync(CancellationToken cancellationToken);

        Task<Portfolio> UpdatePortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken);
    }
}
