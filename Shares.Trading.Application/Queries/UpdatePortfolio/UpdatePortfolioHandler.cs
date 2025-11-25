using MediatR;
using Share.Trading.Application.Infrastructure.Services;

namespace Shares.Trading.Application.Queries.UpdatePortfolio
{
    public class UpdatePortfolioHandler : IRequestHandler<UpdatePortfolioQuery, Share.Trading.Domain.Entities.Models.Portfolio>
    {
        private readonly IPortfolioService _portfolioService;

        public UpdatePortfolioHandler(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        /// <summary>
        /// Handles the update portfolio query.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Updated Portfolio</returns>
        public async Task<Share.Trading.Domain.Entities.Models.Portfolio> Handle(UpdatePortfolioQuery request, CancellationToken cancellationToken)
        {
            return await _portfolioService.UpdatePortfolioAsync(request.portfolio, cancellationToken);
        }
    }
}
