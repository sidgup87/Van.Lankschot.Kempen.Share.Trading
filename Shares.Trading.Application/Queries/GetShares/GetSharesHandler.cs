using MediatR;
using Share.Trading.Application.Infrastructure.Services;
using Share.Trading.Domain.Entities.Models;

namespace Shares.Trading.Application.Queries.GetShares
{
    public class GetSharesHandler : IRequestHandler<GetSharesQuery, List<SharesDetails>>
    {
        private readonly IExchangeService _exchangeService;

        public GetSharesHandler(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;  
        }

        /// <summary>
        /// Handles the GetSharesQuery to retrieve a list of all shares details.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<SharesDetails>> Handle(GetSharesQuery request, CancellationToken cancellationToken)
        {
            return await _exchangeService.GetAllSharesAsync(cancellationToken);
        }
    }
}
