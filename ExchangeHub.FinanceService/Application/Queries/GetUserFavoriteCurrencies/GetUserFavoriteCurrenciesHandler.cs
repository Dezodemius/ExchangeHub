using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Application.DTO;
using ExchangeHub.FinanceService.Application.Interfaces;
using MediatR;

namespace ExchangeHub.FinanceService.Application.Queries.GetUserFavoriteCurrencies;

public class GetUserFavoriteCurrenciesHandler(ICurrencyService currencyService)
    : IRequestHandler<GetUserFavoriteCurrenciesQuery, IList<FavoriteCurrencyDto>>
{
    public async Task<IList<FavoriteCurrencyDto>> Handle(GetUserFavoriteCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return await currencyService.GetUserFavoriteCurrencies(request.UserId, cancellationToken);
    }
}