using Share.Trading.Domain.Entities.Models;

namespace Share.Trading.Application.Infrastructure.Repositories.Exchange
{
    public class VKLExchange : IVKLExchange
    {
        public List<SharesDetails> GetSharesInventories()
        {
            var sharesInventories = new List<SharesDetails>
            {
                new SharesDetails
                {
                    Name = "Apple",
                    Symbol = "AAPL",
                    PricePerShare = 120.00m,
                },
                new SharesDetails
                {
                    Name = "Microsoft",
                    Symbol = "MSFT",
                    PricePerShare = 100.00m,
                },
                new SharesDetails
                {
                    Name = "Amazon",
                    Symbol = "AMZN",
                    PricePerShare = 110.00m,
                }
            };
            return sharesInventories;
        }

    }
}
