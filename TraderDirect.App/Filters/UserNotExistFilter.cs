using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TraderDirect.App.ApiModels.Requests;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Repositories;

namespace TraderDirect.App.Filters;
public class UserNotExistFilter : Attribute, IAsyncActionFilter
{
    private readonly IUserRepository _userRepository;
    public UserNotExistFilter(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("request", out var model) && model is ExecuteTradesRequest request)
        {
            int userId = request.UserId;

            CancellationToken cancellationToken = context.HttpContext.RequestAborted;

            bool userExists = await _userRepository.UserExists(userId, cancellationToken);
            if (!userExists)
            {
                context.Result = new NotFoundObjectResult($"User with ID {userId} does not exist.");
                return;
            }

        }

        await next();
    }
}

