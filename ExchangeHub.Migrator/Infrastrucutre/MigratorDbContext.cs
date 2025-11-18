using ExchangeHub.Migrator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.Migrator.Infrastrucutre;

public class MigratorDbContext(DbContextOptions<MigratorDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    
    public DbSet<Currency> Currencies => Set<Currency>();
    
    public DbSet<UserCurrency> UserCurrencies => Set<UserCurrency>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCurrency>()
            .HasKey(uc => new { uc.UserId, uc.CurrencyId });

        base.OnModelCreating(modelBuilder);
    }
}