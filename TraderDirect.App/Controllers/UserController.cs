using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraderDirect.App.ApiModels.Requests;
using TraderDirect.App.Filters;
using TraderDirect.Domain.Ports.Services;

namespace TraderDirect.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [ServiceFilter(typeof(UserExistsFilter))]
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] CreateUserRequest request,
            [FromServices] ICreateUserService service,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            int id = await service.HandleAsync(request.Email, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, new { Id = id });
        }
    }
}
