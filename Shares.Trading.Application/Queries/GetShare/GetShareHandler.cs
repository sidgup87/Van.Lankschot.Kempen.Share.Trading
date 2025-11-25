using MediatR;
using Share.Trading.Application.Infrastructure.Services;

namespace Shares.Trading.Application.Queries.GetShare
{
    public class GetShareHandler : IRequestHandler<GetShareQuery, Share.Trading.Domain.Entities.Models.SharesDetails>
    {
        private readonly IExchangeService _exchangeService;

        public GetShareHandler(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;  
        }

        /// <summary>
        /// Handles the GetShareQuery to retrieve share details by symbol.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Share Details</returns>
        public async Task<Share.Trading.Domain.Entities.Models.SharesDetails> Handle(GetShareQuery request, CancellationToken cancellationToken)
        {
            return await _exchangeService.GetShareAsync(request.symbol, cancellationToken);
        }
    }
}
