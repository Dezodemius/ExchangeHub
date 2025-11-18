using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.UserService.Application.Interfaces;
using MediatR;

namespace ExchangeHub.UserService.Application.Queries.RemoveFavoriteCurrency;

public class RemoveFavoriteCurrencyHandler(IFavoriteCurrencyService currencyService): IRequestHandler<RemoveFavoriteCurrencyQuery>
{
    public async Task Handle(RemoveFavoriteCurrencyQuery request, CancellationToken cancellationToken)
    {
        await currencyService.RemoveFavoriteCurrencyAsync(request.UserId, request.CurrencyId, cancellationToken);
    }
}