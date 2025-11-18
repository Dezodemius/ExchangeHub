using ExchangeHub.Shared;
using MediatR;

namespace ExchangeHub.UserService.Queries.RegisterUser;

public record RegisterUserQuery(string Username, string Password) : IRequest<User>;
