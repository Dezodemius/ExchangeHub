using System.Collections.Generic;
using ExchangeHub.Shared;
using MediatR;

namespace ExchangeHub.FinanceService.Queries.GetAllCurrencies;

public record GetAllCurrenciesQuery() : IRequest<IList<Currency>>;