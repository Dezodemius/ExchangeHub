using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ExchangeHub.UserService.Queries.AddFavoriteCurrency;

public class AddFavoriteCurrencyHandler(IFavoriteCurrencyService currencyService): IRequestHandler<AddFavoriteCurrencyQuery>
{
    public async Task Handle(AddFavoriteCurrencyQuery request, CancellationToken cancellationToken)
    {
        await currencyService.AddFavoriteCurrencyAsync(request.UserId, request.CurrencyId, cancellationToken);
    }
}