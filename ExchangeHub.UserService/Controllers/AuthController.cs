using System.Threading.Tasks;
using ExchangeHub.UserService.DTO;
using ExchangeHub.UserService.Queries.LoginUser;
using ExchangeHub.UserService.Queries.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeHub.UserService;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator _mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var result = await _mediator.Send(new RegisterUserQuery(dto.Name, dto.Password));
        return Ok(new { result.Id, result.Name });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var result = await _mediator.Send(new LoginUserQuery(dto.Name, dto.Password));
        if (!result.IsSuccess)
            return Unauthorized();
        
        return Ok(new { result.Token });
    }
}