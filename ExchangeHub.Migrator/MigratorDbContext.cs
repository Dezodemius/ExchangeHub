using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.Migrator;

public class MigratorDbContext : DbContext
{
    public MigratorDbContext(DbContextOptions<MigratorDbContext> options)
        : base(options)
    {
    }

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