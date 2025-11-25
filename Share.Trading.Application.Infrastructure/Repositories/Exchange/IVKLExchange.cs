using Share.Trading.Domain.Entities.Models;

namespace Share.Trading.Application.Infrastructure.Repositories.Exchange
{
    public interface IVKLExchange
    {
        List<SharesDetails> GetSharesInventories();
    }
}
