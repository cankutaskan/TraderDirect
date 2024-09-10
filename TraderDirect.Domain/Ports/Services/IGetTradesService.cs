using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Services;
public interface IGetTradesService
{
    Task<List<ITrade>> HandleAsync(int userId, CancellationToken cancellationToken);
    Task<List<ITrade>> HandleAsync(CancellationToken cancellationToken);
}

