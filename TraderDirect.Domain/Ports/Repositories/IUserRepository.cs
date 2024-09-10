using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Repositories;
public interface IUserRepository
{
    Task<int> CreateUserAsync(string email, CancellationToken cancellationToken);

    Task<bool> UserExists(int id, CancellationToken cancellationToken);
}

