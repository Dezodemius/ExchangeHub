using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared;
using ExchangeHub.UserService.Queries.LoginUser;
using MediatR;

namespace ExchangeHub.UserService.Queries.RegisterUser;

public class RegisterUserHandler: IRequestHandler<RegisterUserQuery, User>
{
    private readonly IUserService _userService;

    public RegisterUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<User> Handle(RegisterUserQuery request, CancellationToken ct)
    {
        return await _userService.RegisterAsync(request.Username, request.Password, ct);
    }
    
}