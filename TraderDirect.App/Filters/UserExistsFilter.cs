using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TraderDirect.App.ApiModels.Requests;
using TraderDirect.Domain.Ports.Repositories;

namespace TraderDirect.App.Filters;
    public class UserExistsFilter : Attribute, IAsyncActionFilter
    {
        private readonly IUserRepository _userRepository;
        public UserExistsFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("request", out var model) && model is CreateUserRequest request)
            {
                string email = request.Email;

                CancellationToken cancellationToken = context.HttpContext.RequestAborted;

                bool userExists = await _userRepository.UserExists(email, cancellationToken);
                if (userExists)
                {
                    context.Result = new ConflictObjectResult($"User with email address {email} already exists.");
                    return;
                }

            }

            await next();
        }
    }
