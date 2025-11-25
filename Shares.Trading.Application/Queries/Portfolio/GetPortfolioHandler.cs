using MediatR;
using Share.Trading.Application.Infrastructure.Services;

namespace Shares.Trading.Application.Queries.Portfolio
{
    public class GetPortfolioHandler : IRequestHandler<GetPortfolioQuery, Share.Trading.Domain.Entities.Models.Portfolio>
    {
        private readonly IPortfolioService _portfolioService;

        public GetPortfolioHandler(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        /// <summary>
        /// Handles the GetPortfolioQuery to retrieve the portfolio.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Portfolio</returns>
        public async Task<Share.Trading.Domain.Entities.Models.Portfolio> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
        {
            return await _portfolioService.GetPortfolioAsync(cancellationToken);
        }
    }
}
