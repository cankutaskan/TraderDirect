using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Domain.Ports.Services;

namespace TraderDirect.Domain.Services;
public class CreateUserService(IUserRepository repository) : ICreateUserService
{
    public async Task<int> HandleAsync(string email, CancellationToken cancellationToken)
    {
        return await repository.CreateUserAsync(email, cancellationToken);
    }
}

