namespace TraderDirect.Domain.Ports.Services;
public interface ICreateUserService
{
    Task<int> HandleAsync(string email, CancellationToken cancellationToken);
}

