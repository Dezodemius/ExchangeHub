using System.Collections.Generic;
using ExchangeHub.FinanceService.Application.DTO;
using MediatR;

namespace ExchangeHub.FinanceService.Application.Queries.GetUserFavoriteCurrencies;

public record GetUserFavoriteCurrenciesQuery(int UserId) : IRequest<IList<FavoriteCurrencyDto>>;