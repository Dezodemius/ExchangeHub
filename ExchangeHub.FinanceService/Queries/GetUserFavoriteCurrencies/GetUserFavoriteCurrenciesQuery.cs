using System.Collections.Generic;
using ExchangeHub.Shared.DTO;
using MediatR;

namespace ExchangeHub.FinanceService.Queries.GetUserFavoriteCurrencies;

public record GetUserFavoriteCurrenciesQuery(long UserId) : IRequest<IList<CurrencyDto>>;