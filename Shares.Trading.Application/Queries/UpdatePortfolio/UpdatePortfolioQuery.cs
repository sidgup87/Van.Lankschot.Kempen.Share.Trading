using MediatR;

namespace Shares.Trading.Application.Queries.UpdatePortfolio
{
    public record UpdatePortfolioQuery(Share.Trading.Domain.Entities.Models.Portfolio portfolio) : IRequest<Share.Trading.Domain.Entities.Models.Portfolio>;

}
