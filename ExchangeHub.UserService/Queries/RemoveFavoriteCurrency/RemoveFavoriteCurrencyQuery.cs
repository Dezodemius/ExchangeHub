using MediatR;

namespace ExchangeHub.UserService.Queries.DeleteFavoriteCurrency;

public record RemoveFavoriteCurrencyQuery(int UserId, int CurrencyId) : IRequest;
