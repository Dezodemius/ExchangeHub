using ExchangeHub.Shared;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.UserService;

public interface IUserServiceDbContext
{
    DbSet<User> Users { get; }
}