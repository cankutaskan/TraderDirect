using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb.Repositories;
public class UserRepository(TraderDirectDbContext dbContext) : IUserRepository
{
    private readonly TraderDirectDbContext _dbContext = dbContext;

    public async Task<int> CreateUserAsync(string email, CancellationToken cancellationToken)
    {
        UserEntity entity = new() { Email = email };
        await _dbContext.Users.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<bool> UserExists(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(x => x.Id == id, cancellationToken);
    }
}

