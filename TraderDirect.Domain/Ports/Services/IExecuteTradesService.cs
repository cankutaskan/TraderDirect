using TraderDirect.Domain.Models;
using static TraderDirect.Domain.Ports.Repositories.ITradesRepository;

namespace TraderDirect.Domain.Ports.Services;
public interface IExecuteTradesService
{
    Task HandleAsync(List<ExecuteTradeServiceRequest> Trades, CancellationToken cancellationToken);
}

