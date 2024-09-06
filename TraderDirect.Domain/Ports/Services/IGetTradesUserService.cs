using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Services;
public interface IGetTradesUserService
{
    List<ITrade> Handle(int userId);
}

