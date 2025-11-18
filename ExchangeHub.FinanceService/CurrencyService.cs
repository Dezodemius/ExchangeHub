using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Queryable;

namespace ExchangeHub.FinanceService;

public class CurrencyService : ICurrencyService
{
    private readonly IFinanceServiceDbContext _db;

    public CurrencyService(IFinanceServiceDbContext db)
    {
        _db = db;
    }

    public async Task<IList<Currency>> GetAllCurrencies(CancellationToken ct)
    {
        return await _db.Currencies.ToListAsync(ct);
    }

    public async Task<IList<UserCurrency>> GetUserFavoriteCurrencies(long id, CancellationToken ct)
    {
        return await _db.UserCurrencies
            .Where(uc => uc.UserId == id)
            .ToListAsync(ct);
    }
}