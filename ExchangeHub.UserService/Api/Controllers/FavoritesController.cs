using System.Security.Claims;
using System.Threading.Tasks;
using ExchangeHub.UserService.Application.DTO;
using ExchangeHub.UserService.Application.Queries.AddFavoriteCurrency;
using ExchangeHub.UserService.Application.Queries.RemoveFavoriteCurrency;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeHub.UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController(IMediator _mediator) : ControllerBase
{
    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddFavoriteCurrency(UserCurrencyDto currencyDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _mediator.Send(new AddFavoriteCurrencyQuery(userId, currencyDto.CurrencyId));
        return NoContent();
    }

    [Authorize]
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveFavoriteCurrency(UserCurrencyDto currencyDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _mediator.Send(new RemoveFavoriteCurrencyQuery(userId, currencyDto.CurrencyId));
        return NoContent();
    }
}