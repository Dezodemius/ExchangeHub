using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.UserService.Application.Interfaces;
using MediatR;

namespace ExchangeHub.UserService.Application.Queries.LoginUser;

public class LoginUserHandler(IAuthService authService, IJwtService jwtService)
    : IRequestHandler<LoginUserQuery, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserQuery request, CancellationToken ct)
    {
        var user = await authService.AuthenticateAsync(request.Username, request.Password, ct); 
        if (user == null) 
            return new LoginUserResult(false, null);
        
        var token = jwtService.GenerateJwtToken(user);
        return new LoginUserResult(true, token);
    }
}