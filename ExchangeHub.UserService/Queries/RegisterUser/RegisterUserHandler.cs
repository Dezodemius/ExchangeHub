using System.Threading;
using System.Threading.Tasks;
using MediatR;
using User = ExchangeHub.UserService.Models.User;

namespace ExchangeHub.UserService.Queries.RegisterUser;

public class RegisterUserHandler: IRequestHandler<RegisterUserQuery, User>
{
    private readonly IAuthService _authService;

    public RegisterUserHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<User> Handle(RegisterUserQuery request, CancellationToken ct)
    {
        return await _authService.RegisterAsync(request.Username, request.Password, ct);
    }
    
}