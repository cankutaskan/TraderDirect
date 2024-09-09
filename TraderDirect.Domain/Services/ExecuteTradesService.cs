using TraderDirect.Domain.Models;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Domain.Ports.Services;
using static TraderDirect.Domain.Ports.Repositories.ITradesRepository;

namespace TraderDirect.Domain.Services;
public class ExecuteTradesService(ITradesRepository repository) : IExecuteTradesService
{
    private readonly ITradesRepository _repository = repository;

    public async Task HandleAsync(List<ExecuteTradeServiceRequest> trades, CancellationToken cancellationToken)
    {
        await _repository.CreateTrades(trades, cancellationToken);
    }
}

