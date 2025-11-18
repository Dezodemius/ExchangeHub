using ExchangeHub.UserService.Models;
using Microsoft.EntityFrameworkCore;
using User = ExchangeHub.UserService.Models.User;

namespace ExchangeHub.UserService;

public class UserServiceDbContext : DbContext, IUserServiceDbContext
{
    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<UserCurrency> UserCurrencies => Set<UserCurrency>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<UserCurrency>()
            .HasKey(x => new { x.UserId, x.CurrencyId });


        base.OnModelCreating(modelBuilder);
    }
}
