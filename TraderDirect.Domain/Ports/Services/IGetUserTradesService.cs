using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Services;
public interface IGetUserTradesService
{
    Task<List<ITrade>> HandleAsync(int userId, CancellationToken cancellationToken);
}

