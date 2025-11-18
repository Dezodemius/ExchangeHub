using System.Threading;
using System.Threading.Tasks;

namespace ExchangeHub.UserService.Application.Interfaces;

public interface IFavoriteCurrencyService
{
    Task AddFavoriteCurrencyAsync(int userId, int currencyId, CancellationToken ct);

    Task RemoveFavoriteCurrencyAsync(int userId, int currencyId, CancellationToken ct);
}