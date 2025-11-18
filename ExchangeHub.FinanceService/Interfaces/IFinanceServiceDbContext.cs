using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Models;
using Microsoft.EntityFrameworkCore;
using Currency = ExchangeHub.FinanceService.Models.Currency;

namespace ExchangeHub.FinanceService;

public interface IFinanceServiceDbContext
{
    DbSet<Currency> Currencies { get; }

    DbSet<UserCurrency> UserCurrencies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}