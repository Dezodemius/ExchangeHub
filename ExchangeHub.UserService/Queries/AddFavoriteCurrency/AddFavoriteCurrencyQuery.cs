using MediatR;

namespace ExchangeHub.UserService.Queries.AddFavoriteCurrency;

public record AddFavoriteCurrencyQuery(int UserId, int CurrencyId) : IRequest;