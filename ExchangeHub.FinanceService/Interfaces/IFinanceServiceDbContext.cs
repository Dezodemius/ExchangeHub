using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.FinanceService;

public interface IFinanceServiceDbContext
{
    DbSet<Currency> Currencies { get; }

    DbSet<UserCurrency> UserCurrencies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}