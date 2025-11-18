using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.UserService.Application.Interfaces;
using MediatR;
using Entities_User = ExchangeHub.UserService.Domain.Entities.User;
using User = ExchangeHub.UserService.Domain.Entities.User;

namespace ExchangeHub.UserService.Application.Queries.RegisterUser;

public class RegisterUserHandler(IAuthService authService) : IRequestHandler<RegisterUserQuery, Entities_User>
{
    public async Task<User> Handle(RegisterUserQuery request, CancellationToken ct)
    {
        return await authService.RegisterAsync(request.Username, request.Password, ct);
    }
    
}