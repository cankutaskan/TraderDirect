using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;
public class UserEntity : IUser
{
    public int Id { get; set; }
    public required string Email { get; set; }
}

