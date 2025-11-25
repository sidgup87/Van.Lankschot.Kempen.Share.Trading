using Share.Trading.Application.Infrastructure.Repositories.Exchange;
using Share.Trading.Application.Infrastructure.Repositories.Portfolio;
using Share.Trading.Domain.Entities.Models;

namespace Share.Trading.Application.Infrastructure.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioService(IPortfolioRepository portfolioRepository) 
        {
            _portfolioRepository = portfolioRepository;
        }
        public async Task<Portfolio> GetPortfolioAsync(CancellationToken cancellationToken)
        {
            return await _portfolioRepository.GetPortfolio(cancellationToken);
        }

        public async Task<Portfolio> UpdatePortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken)
        {
            return await _portfolioRepository.UpdatePortfolio(portfolio, cancellationToken);
        }
    }
}
