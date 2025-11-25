using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Share.Trading.Application.Infrastructure.Repositories.Portfolio;
using Share.Trading.Domain.Entities.Models;

namespace Van.Lankschot.Kempen.Api.Tests.Controllers
{
    public class PortfolioServiceRepoTests
    {
        private readonly Mock<ILogger<PortfolioRepository>> _loggerMock;
        private readonly PortfolioRepository _repository;


        public PortfolioServiceRepoTests()
        {
            _loggerMock = new Mock<ILogger<PortfolioRepository>>();
            _repository = new PortfolioRepository(_loggerMock.Object);
        }

        [Fact]
        public async Task GetPortfolio_ShouldReturnInitialPortfolio()
        {
            // Act
            var portfolio = await _repository.GetPortfolio(CancellationToken.None);

            // Assert
            portfolio.Should().NotBeNull();
            portfolio.CashBalance.Should().Be(10000);
            portfolio.Shares.Should().HaveCount(1);
            portfolio.Shares[0].Symbol.Should().Be("AAPL");
        }

        [Fact]
        public async Task UpdatePortfolio_ShouldModifyPortfolioCashAndShares()
        {
            // Arrange
            var newPortfolio = new Portfolio
            {
                Name = "Siddharth Gupta",
                CashBalance = 5000,
                Shares = new List<SharesDetails>
                {
                    new SharesDetails
                    {
                        Name = "Tesla",
                        Symbol = "TSLA",
                        Quantity = 10
                    }
                }
            };

            // Act
            var updatedPortfolio = await _repository.UpdatePortfolio(newPortfolio, CancellationToken.None);

            // Assert
            updatedPortfolio.Should().NotBeNull();
            updatedPortfolio.CashBalance.Should().Be(5000);
            updatedPortfolio.Shares.Should().HaveCount(1);
            updatedPortfolio.Shares[0].Symbol.Should().Be("TSLA");

            // Verify GetPortfolio returns updated values
            var getPortfolio = await _repository.GetPortfolio(CancellationToken.None);
            getPortfolio.CashBalance.Should().Be(5000);
            getPortfolio.Shares[0].Symbol.Should().Be("TSLA");
        }
    }
}
