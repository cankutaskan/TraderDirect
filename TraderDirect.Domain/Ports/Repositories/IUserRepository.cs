using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Repositories;
public interface IUserRepository
{
    /// <summary>
    /// Create user in database
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>ID of the added user</returns>
    Task<int> CreateUserAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Check if user exists in database
    /// </summary>
    /// <param name="id">ID of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>true if exist else false</returns>
    Task<bool> UserExists(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Check if user exists in database
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>true if exist else false</returns>
    Task<bool> UserExists(string email, CancellationToken cancellationToken);
}

