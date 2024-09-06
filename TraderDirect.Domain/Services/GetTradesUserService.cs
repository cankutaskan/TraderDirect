using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.IServices;

namespace TraderDirect.Domain.Services;
public class GetTradesUserService : IGetTradesUserService
{
    public List<ITrade> Handle(int userId)
    {
        throw new NotImplementedException();
    }
}

