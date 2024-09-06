using Azure;
using Microsoft.AspNetCore.Mvc;
using TraderDirect.App.Responses;
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
    }
}
