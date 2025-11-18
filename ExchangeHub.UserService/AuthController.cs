using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExchangeHub.Shared;
using ExchangeHub.UserService.DTO;
using ExchangeHub.UserService.Queries.LoginUser;
using ExchangeHub.UserService.Queries.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExchangeHub.UserService;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var result = await mediator.Send(new RegisterUserQuery(dto.Name, dto.Password));
        return Ok(new { result.Id, result.Name });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var result = await mediator.Send(new LoginUserQuery(dto.Name, dto.Password));
        if (!result.IsSuccess)
            return Unauthorized();
        
        return Ok(new { result.Token });
    }
}