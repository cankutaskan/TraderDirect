using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Domain.Ports.Services;

namespace TraderDirect.Domain.Services;
public class GetUserTradesService(ITradesRepository repository) : IGetUserTradesService
{
    private readonly ITradesRepository _repository = repository;

    public async Task<List<ITrade>> HandleAsync(int userId, CancellationToken cancellationToken)
    {
        return (await _repository.GetUserTrades(userId)).ToList();
    }
}

