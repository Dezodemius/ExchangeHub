using ExchangeHub.UserService.Application.Interfaces;
using ExchangeHub.UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using User = ExchangeHub.UserService.Domain.Entities.User;

namespace ExchangeHub.UserService.Infrasturcture;

public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
    : DbContext(options), IUserServiceDbContext
{
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
