using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TraderDirect.App.ApiModels.Requests;
using TraderDirect.App.ApiModels.Responses;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Services;
using static TraderDirect.Domain.Ports.Repositories.ITradesRepository;

namespace TraderDirect.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController(ILogger<TradesController> logger) : ControllerBase
    {
        private readonly ILogger<TradesController> _logger = logger;

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(
          int userId,
          [FromServices] IGetTradesService service,
          CancellationToken cancellationToken)
        {
            List<ITrade> trades = await service.HandleAsync(userId, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, new TradesUserResponse(trades));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
          [FromServices] IGetTradesService service,
          CancellationToken cancellationToken)
        {
            List<ITrade> trades = await service.HandleAsync(cancellationToken);
            return StatusCode(StatusCodes.Status200OK, new TradesUserResponse(trades));
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] ExecuteTradesRequest request,
            [FromServices] IExecuteTradesService service,
            CancellationToken cancellationToken)
        {
            string requetsJson = JsonSerializer.Serialize(request);
            _logger.LogInformation("Request:{0}", requetsJson);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            List<ExecuteTradeServiceRequest> serviceTrades = request.Trades
                .Select(trade => new ExecuteTradeServiceRequest
                {
                    UserId = request.UserId,
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
