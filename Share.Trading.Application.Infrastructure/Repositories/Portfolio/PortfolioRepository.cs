using Microsoft.Extensions.Logging;
using Share.Trading.Domain.Entities.Models;

namespace Share.Trading.Application.Infrastructure.Repositories.Portfolio
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private Domain.Entities.Models.Portfolio _portfolio;
        private readonly ILogger<PortfolioRepository> _logger;

        public PortfolioRepository(ILogger<PortfolioRepository> logger)
        {
            // Initialize with some default values
            _portfolio = new Domain.Entities.Models.Portfolio
            {
                Name = "Siddharth Gupta",
                CashBalance = 10000,
                Shares = new List<SharesDetails>()
                {
                    new SharesDetails
                    {
                        Name = "Apple",
                        Symbol = "AAPL",
                        Quantity = 0
                    }
                }  
            };

            _logger = logger;

        }

        /// <summary>
        /// Get the current portfolio details
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Portfolio</returns>
        public async Task<Domain.Entities.Models.Portfolio> GetPortfolio(CancellationToken token)
        {
            _logger.LogInformation($"Fetching portfolio with CashBalance: {_portfolio.CashBalance}");
            return await Task.FromResult(_portfolio);
        }

        /// <summary>
        /// Update the portfolio details
        /// </summary>
        /// <param name="portfolio"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Updated Portfolio</returns>
        public async Task<Domain.Entities.Models.Portfolio> UpdatePortfolio(Domain.Entities.Models.Portfolio portfolio, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating portfolio to CashBalance: {portfolio.CashBalance}");
            this._portfolio = portfolio;

            return await Task.FromResult(_portfolio);

        }
    }
}
