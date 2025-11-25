using MediatR;

namespace Shares.Trading.Application.Queries.Portfolio
{
    public record GetPortfolioQuery() : IRequest<Share.Trading.Domain.Entities.Models.Portfolio>;

}
