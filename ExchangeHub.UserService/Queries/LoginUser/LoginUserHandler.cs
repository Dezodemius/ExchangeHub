using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ExchangeHub.UserService.Queries.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, LoginUserResult>
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public LoginUserHandler(IAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    public async Task<LoginUserResult> Handle(LoginUserQuery request, CancellationToken ct)
    {
        var user = await _authService.AuthenticateAsync(request.Username, request.Password, ct); 
        if (user == null) 
            return new LoginUserResult(false, null);
        
        var token = _jwtService.GenerateJwtToken(user);
        return new LoginUserResult(true, token);
    }
}