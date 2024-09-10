using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Domain.Ports.Services;

namespace TraderDirect.Domain.Services;
public class GetTradesService(ITradesRepository repository) : IGetTradesService
{
    private readonly ITradesRepository _repository = repository;

    public async Task<List<ITrade>> HandleAsync(int userId, CancellationToken cancellationToken)
    {
        return (await _repository.GetUserTrades(userId, cancellationToken)).ToList();
    }

    public async Task<List<ITrade>> HandleAsync(CancellationToken cancellationToken)
    {
        return (await _repository.GetAllTrades(cancellationToken)).ToList();
    }
}

