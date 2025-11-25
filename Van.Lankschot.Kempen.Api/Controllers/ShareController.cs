using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shares.Trading.Application.Exceptions;
using Shares.Trading.Application.Queries.GetShare;
using Shares.Trading.Application.Queries.GetShares;
using Van.Lankschot.Kempen.Api.Filters;

namespace Van.Lankschot.Kempen.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ShareController> _logger;

        public ShareController(IMediator mediator, ILogger<ShareController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// Get the Share details by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Share Details object</returns>
        [HttpGet("{symbol}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetShareDetails(string symbol)
        {
            var shareDetails = await _mediator.Send(new GetShareQuery(symbol));
            if (shareDetails == null)
            {
                throw new NotFoundException("Share not found in exchange.");
            }
            return Ok(shareDetails);
        }

        /// <summary>
        /// Get all Shares
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllShares()
        {
            var shares = await _mediator.Send(new GetSharesQuery());
            return Ok(shares);
        }

    }
}
