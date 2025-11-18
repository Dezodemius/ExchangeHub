using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.UserService.Application.Interfaces;
using ExchangeHub.UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.UserService.Application.Services;

public class FavoriteCurrencyService(IUserServiceDbContext db) : IFavoriteCurrencyService
{
    public async Task AddFavoriteCurrencyAsync(int userId, int currencyId, CancellationToken ct)
    {
        bool alreadyAdded = await db.UserCurrencies
            .AnyAsync(x =>
                    x.UserId == userId &&
                    x.CurrencyId == currencyId,
                ct);

        if (!alreadyAdded)
        {
            db.UserCurrencies.Add(new UserCurrency
            {
                UserId = userId,
                CurrencyId = currencyId
            });

            await db.SaveChangesAsync(ct);
        }
    }

    public async Task RemoveFavoriteCurrencyAsync(int userId, int currencyId, CancellationToken ct)
    {
        var existingCurrency = await db.UserCurrencies
            .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.CurrencyId == currencyId,
                ct);

        if (existingCurrency is null)
            return;

        db.UserCurrencies.Remove(existingCurrency);
        await db.SaveChangesAsync(ct);
    }
}