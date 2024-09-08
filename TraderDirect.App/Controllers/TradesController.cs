using Azure;
using Microsoft.AspNetCore.Mvc;
using TraderDirect.App.ApiModels.Requests;
using TraderDirect.App.ApiModels.Responses;
using TraderDirect.Domain.Models;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Services;

namespace TraderDirect.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(
          int userId,
          [FromServices] IGetUserTradesService service,
          CancellationToken cancellationToken)
        {
            List<ITrade> trades = await service.HandleAsync(userId, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, new TradesUserResponse(trades));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] ExecuteTradesRequest request,
            [FromServices] IExecuteTradesService service,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            List<ExecuteTradeServiceRequest> serviceTrades = request.Trades
                .Select(trade => new ExecuteTradeServiceRequest
                {
                    UserId = trade.UserId,
                    Asset = trade.Asset,
                    Quantity = trade.Quantity,
                    Price = trade.Price
                })
                .ToList();

            await service.HandleAsync(serviceTrades, cancellationToken);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
