using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Share.Trading.Application.Infrastructure.Repositories.Exchange;
using Share.Trading.Application.Infrastructure.Services;
using Share.Trading.Domain.Entities.Models;
using Xunit;

namespace Van.Lankschot.Kempen.Api.Tests.Controllers
{
    public class ExchangeServiceRepo
    {
        private readonly Mock<IVKLExchange> _vklExchangeMock;
        private readonly ExchangeService _exchangeService;

        public ExchangeServiceRepo()
        {
            _vklExchangeMock = new Mock<IVKLExchange>();
            _exchangeService = new ExchangeService(_vklExchangeMock.Object);
        }

        [Fact]
        public async Task GetShareAsync_ShouldReturnShare_WhenExists()
        {
            // Arrange
            var shares = new List<SharesDetails>
            {
                new SharesDetails { Symbol = "AAPL", Name = "Apple", PricePerShare = 100 },
                new SharesDetails { Symbol = "TSLA", Name = "Tesla", PricePerShare = 200 }
            };

            _vklExchangeMock
                .Setup(x => x.GetSharesInventories())
                .Returns(shares);

            // Act
            var result = await _exchangeService.GetShareAsync("AAPL", CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Symbol.Should().Be("AAPL");
            result.Name.Should().Be("Apple");
        }

        [Fact]
        public async Task GetShareAsync_ShouldReturnNull_WhenShareDoesNotExist()
        {
            // Arrange
            _vklExchangeMock
                .Setup(x => x.GetSharesInventories())
                .Returns(new List<SharesDetails>());

            // Act
            var result = await _exchangeService.GetShareAsync("FAKE", CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllSharesAsync_ShouldReturnAllShares()
        {
            // Arrange
            var shares = new List<SharesDetails>
            {
                new SharesDetails { Symbol = "AAPL", Name = "Apple", PricePerShare = 100 },
                new SharesDetails { Symbol = "TSLA", Name = "Tesla", PricePerShare = 200 }
            };

            _vklExchangeMock
                .Setup(x => x.GetSharesInventories())
                .Returns(shares);

            // Act
            var result = await _exchangeService.GetAllSharesAsync(CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(shares);
        }
    }
}
