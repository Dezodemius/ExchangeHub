using ExchangeHub.Shared;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.FinanceService;

public class FinanceServiceDbContext : DbContext, IFinanceServiceDbContext
{
    public DbSet<Currency> Currencies => Set<Currency>();
    
    public DbSet<UserCurrency> UserCurrencies => Set<UserCurrency>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCurrency>()
            .HasKey(uc => new { uc.UserId, uc.CurrencyId });

        modelBuilder.Entity<UserCurrency>()
            .HasOne(uc => uc.Currency)
            .WithMany()
            .HasForeignKey(uc => uc.CurrencyId);

        base.OnModelCreating(modelBuilder);
    }

    
    public FinanceServiceDbContext(DbContextOptions<FinanceServiceDbContext> options)
        : base(options) { }
}