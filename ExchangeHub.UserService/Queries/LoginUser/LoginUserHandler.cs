using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ExchangeHub.UserService.Queries.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, LoginUserResult>
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public LoginUserHandler(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<LoginUserResult> Handle(LoginUserQuery request, CancellationToken ct)
    {
        var user = await _userService.AuthenticateAsync(request.Username, request.Password, ct); 
        if (user == null) 
            return new LoginUserResult(false, null);
        
        var token = _jwtService.GenerateJwtToken(user);
        return new LoginUserResult(true, token);
    }
}