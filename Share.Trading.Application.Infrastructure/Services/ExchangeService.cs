using Share.Trading.Application.Infrastructure.Repositories.Exchange;
using Share.Trading.Domain.Entities.Models;

namespace Share.Trading.Application.Infrastructure.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IVKLExchange _vklExchangeRepository;
        public ExchangeService(IVKLExchange vklExchangeRepository) 
        {
            _vklExchangeRepository = vklExchangeRepository;
        }

        public async Task<SharesDetails> GetShareAsync(string symbol, CancellationToken cancellationToken)
        {
            var share = _vklExchangeRepository.GetSharesInventories()
                               .FirstOrDefault(s => s.Symbol == symbol);
            return await Task.FromResult(share);
        }
        public async Task<List<SharesDetails>> GetAllSharesAsync(CancellationToken cancellationToken)
        {
            var shares = _vklExchangeRepository.GetSharesInventories().ToList();
            return await Task.FromResult(shares);
        }
    }
}
