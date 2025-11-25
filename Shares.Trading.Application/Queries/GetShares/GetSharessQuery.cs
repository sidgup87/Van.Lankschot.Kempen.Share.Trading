using MediatR;
using Share.Trading.Domain.Entities.Models;


namespace Shares.Trading.Application.Queries.GetShares
{
    public record GetSharesQuery() : IRequest<List<SharesDetails>>;

}
