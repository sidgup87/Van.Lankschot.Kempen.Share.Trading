using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Share.Trading.Domain.Entities.Models;
using Shares.Trading.Application.Queries.GetShare;
using Shares.Trading.Application.Queries.Portfolio;
using Shares.Trading.Application.Queries.UpdatePortfolio;
using Van.Lankschot.Kempen.Api.Controllers;

namespace Van.Lankschot.Kempen.Api.Tests.Controllers
{
    public class TradingControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<TradingController>> _loggerMock;
        private readonly TradingController _controller;

        public TradingControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<TradingController>>();
            _controller = new TradingController(_mediatorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetPortfolio_ShouldReturnOkWithPortfolio()
        {
            // Arrange
            var portfolio = new Portfolio
            {
                CashBalance = 1000,
                Shares = new List<SharesDetails>()
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPortfolioQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(portfolio);

            // Act
            var result = await _controller.GetPortfolio();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = result as OkObjectResult;
            ok!.Value.Should().BeEquivalentTo(portfolio);
        }

        [Fact]
        public async Task BuyShare_ShouldReturnNotFound_WhenShareNotFound()
        {
            // Arrange
            var request = new TradingRequest
            {
                Symbol = "FAKE",
                Shares = 5,
                PricePerShare = 100
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPortfolioQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Portfolio { CashBalance = 1000, Shares = new List<SharesDetails>() });

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetShareQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SharesDetails)null); // share not found

            // Act
            var result = await _controller.BuyShare(request);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task BuyShare_ShouldReturnBadRequest_WhenPriceMismatch()
        {
            // Arrange
            var request = new TradingRequest
            {
                Symbol = "AAPL",
                Shares = 5,
                PricePerShare = 100
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPortfolioQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Portfolio { CashBalance = 1000, Shares = new List<SharesDetails>() });

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetShareQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SharesDetails { Symbol = "AAPL", PricePerShare = 200, Name = "Apple" });

            // Act
            var result = await _controller.BuyShare(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task BuyShare_ShouldReturnBadRequest_WhenInsufficientFunds()
        {
            // Arrange
            var request = new TradingRequest
            {
                Symbol = "AAPL",
                Shares = 10,
                PricePerShare = 100
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPortfolioQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Portfolio { CashBalance = 500, Shares = new List<SharesDetails>() });

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetShareQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SharesDetails { Symbol = "AAPL", PricePerShare = 100, Name = "Apple" });

            // Act
            var result = await _controller.BuyShare(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task BuyShare_ShouldReturnOk_WhenValidRequest()
        {
            // Arrange
            var request = new TradingRequest
            {
                Symbol = "AAPL",
                Shares = 5,
                PricePerShare = 100
            };

            var portfolio = new Portfolio { CashBalance = 1000, Shares = new List<SharesDetails>() };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPortfolioQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(portfolio);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetShareQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SharesDetails { Symbol = "AAPL", PricePerShare = 100, Name = "Apple" });

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdatePortfolioQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(portfolio);

            // Act
            var result = await _controller.BuyShare(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = result as OkObjectResult;
            ok!.Value.Should().BeEquivalentTo(portfolio);
        }
    }
}
