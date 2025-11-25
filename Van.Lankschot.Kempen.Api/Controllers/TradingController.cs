using MediatR;
using Microsoft.AspNetCore.Mvc;
using Share.Trading.Domain.Entities.Models;
using Shares.Trading.Application.Exceptions;
using Shares.Trading.Application.Queries.GetShare;
using Shares.Trading.Application.Queries.Portfolio;
using Shares.Trading.Application.Queries.UpdatePortfolio;
using System.Net;

namespace Van.Lankschot.Kempen.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TradingController> _logger;

        public TradingController(IMediator mediator, ILogger<TradingController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("portfolio")]
        [ProducesResponseType(typeof(Portfolio), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Portfolio), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPortfolio()
        {
            var result = await _mediator.Send(new GetPortfolioQuery());
            return Ok(result);
        }
        /// <summary>
        ///  Api for buying the shares
        /// </summary>
        /// <param name="request">trade request </param>
        /// <returns>Updated Portfolio</returns>
        [HttpPost("buy")]
        [ProducesResponseType(typeof(Portfolio), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Portfolio), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> BuyShare([FromBody] TradingRequest request)
        {
            _logger.LogInformation($"Received buy request: {request.Shares} shares at {request.PricePerShare} each.");

            var portfolio = await _mediator.Send(new GetPortfolioQuery());

            var shareDetails = await _mediator.Send(new GetShareQuery(request.Symbol));
            if (shareDetails == null)
            {
                throw new NotFoundException("Share not found in exchange.");
            }
            else if (shareDetails.PricePerShare != request.PricePerShare)
            {
                throw new BadRequestException("Trading cannot be processed due to price mismatch");
            }
            var totalCost = request.Shares * shareDetails.PricePerShare;
            // Validation: sufficient funds
            if (totalCost > portfolio.CashBalance)
            {
                throw new BadRequestException($"Insufficient funds, available = {portfolio.CashBalance}, required = {totalCost}" );
            }
            // Perform the trade
            portfolio.CashBalance -= totalCost;

            var portfolioShareExists = portfolio.Shares.FindIndex(p => p.Symbol.Equals(request.Symbol));

            if (portfolioShareExists != -1)
            {
                portfolio.Shares[portfolioShareExists].Quantity += request.Shares;
                portfolio.Shares[portfolioShareExists].PricePerShare += request.PricePerShare;
            }
            else
            {
                portfolio.Shares.Add(new SharesDetails()
                {
                    Symbol = request.Symbol,
                    PricePerShare = shareDetails.PricePerShare,
                    Quantity = request.Shares,
                    Name = shareDetails.Name
                });
            }

            var result = await _mediator.Send(new UpdatePortfolioQuery(portfolio));

            return Ok(result);

        }
    }
}
