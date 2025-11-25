namespace Share.Trading.Application.Infrastructure.Repositories.Portfolio
{
    public interface IPortfolioRepository
    {        
        Task<Domain.Entities.Models.Portfolio> GetPortfolio(CancellationToken cancellationToken);
        Task<Domain.Entities.Models.Portfolio> UpdatePortfolio(Domain.Entities.Models.Portfolio portfolio, CancellationToken cancellationToken);
    }
}
