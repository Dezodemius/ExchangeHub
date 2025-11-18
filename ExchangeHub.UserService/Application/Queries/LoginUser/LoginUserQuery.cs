using MediatR;

namespace ExchangeHub.UserService.Application.Queries.LoginUser;

public record LoginUserQuery(string Username, string Password) : IRequest<LoginUserResult>;