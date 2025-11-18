using ExchangeHub.Shared;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.UserService;

public class UserServiceDbContext : DbContext
{
    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
}
