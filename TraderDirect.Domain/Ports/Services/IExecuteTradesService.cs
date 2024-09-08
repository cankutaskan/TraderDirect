using TraderDirect.Domain.Models;

namespace TraderDirect.Domain.Ports.Services;
public interface IExecuteTradesService
{
    Task HandleAsync(List<ExecuteTradeServiceRequest> Trades, CancellationToken cancellationToken);
}

