using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Share.Trading.Domain.Entities.Models;
using Shares.Trading.Application.Queries.GetShare;
using Shares.Trading.Application.Queries.GetShares;
using Van.Lankschot.Kempen.Api.Controllers;

namespace Van.Lankschot.Kempen.Api.Tests.Controllers
{
    public class ShareControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<ShareController>> _loggerMock;
        private readonly ShareController _controller;

        public ShareControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<ShareController>>();
            _controller = new ShareController(_mediatorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetShareDetails_ShouldReturnOk_WhenShareExists()
        {
            // Arrange
            var symbol = "AAPL";
            var shareDetails = new SharesDetails
            {
                Symbol = symbol,
                Name = "Apple",
                PricePerShare = 100,
                Quantity = 10
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetShareQuery>(q => q.symbol == symbol), It.IsAny<CancellationToken>()))
                .ReturnsAsync(shareDetails);

            // Act
            var result = await _controller.GetShareDetails(symbol);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = result as OkObjectResult;
            ok!.Value.Should().BeEquivalentTo(shareDetails);
        }

        [Fact]
        public async Task GetShareDetails_ShouldReturnNotFound_WhenShareDoesNotExist()
        {
            // Arrange
            var symbol = "FAKE";

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetShareQuery>(q => q.symbol == symbol), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SharesDetails)null);

            // Act
            var result = await _controller.GetShareDetails(symbol);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var badRequest = result as NotFoundObjectResult;
            badRequest!.Value.Should().BeEquivalentTo("Share not found in exchange.");
        }

        [Fact]
        public async Task GetAllShares_ShouldReturnOk_WithShareList()
        {
            // Arrange
            var shares = new List<SharesDetails>
            {
                new SharesDetails { Symbol = "AAPL", Name = "Apple", PricePerShare = 100 },
                new SharesDetails { Symbol = "TSLA", Name = "Tesla", PricePerShare = 200 }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetSharesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(shares);

            // Act
            var result = await _controller.GetAllShares();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = result as OkObjectResult;
            ok!.Value.Should().BeEquivalentTo(shares);
        }
    }
}
