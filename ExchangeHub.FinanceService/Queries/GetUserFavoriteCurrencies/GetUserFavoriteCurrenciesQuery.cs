using System.Collections.Generic;
using ExchangeHub.FinanceService.DTO;
using ExchangeHub.FinanceService.Models;
using MediatR;

namespace ExchangeHub.FinanceService.Queries.GetUserFavoriteCurrencies;

public record GetUserFavoriteCurrenciesQuery(int UserId) : IRequest<IList<FavoriteCurrencyDto>>;