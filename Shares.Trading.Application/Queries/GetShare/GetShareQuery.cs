using MediatR;


namespace Shares.Trading.Application.Queries.GetShare
{
    public record GetShareQuery(string symbol) : IRequest<Share.Trading.Domain.Entities.Models.SharesDetails>;

}
