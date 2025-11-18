using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.UserService.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.UserService;

public class FavoriteCurrencyService : IFavoriteCurrencyService
{
    private readonly IUserServiceDbContext _db;

    public FavoriteCurrencyService(IUserServiceDbContext db)
    {
        _db = db;
    }
    
    public async Task AddFavoriteCurrencyAsync(int userId, int currencyId, CancellationToken ct)
    {
        bool alreadyAdded = await _db.UserCurrencies
            .AnyAsync(x =>
                    x.UserId == userId &&
                    x.CurrencyId == currencyId,
                ct);

        if (!alreadyAdded)
        {
            _db.UserCurrencies.Add(new UserCurrency
            {
                UserId = userId,
                CurrencyId = currencyId
            });

            await _db.SaveChangesAsync(ct);
        }
    }

    public async Task RemoveFavoriteCurrencyAsync(int userId, int currencyId, CancellationToken ct)
    {
        var existingCurrency = await _db.UserCurrencies
            .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.CurrencyId == currencyId,
                ct);

        if (existingCurrency is null)
            return;

        _db.UserCurrencies.Remove(existingCurrency);
        await _db.SaveChangesAsync(ct);
    }
}