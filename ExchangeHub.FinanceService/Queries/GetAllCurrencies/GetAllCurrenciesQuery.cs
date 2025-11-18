using System.Collections.Generic;
using MediatR;
using Currency = ExchangeHub.FinanceService.Models.Currency;

namespace ExchangeHub.FinanceService.Queries.GetAllCurrencies;

public record GetAllCurrenciesQuery() : IRequest<IList<Currency>>;