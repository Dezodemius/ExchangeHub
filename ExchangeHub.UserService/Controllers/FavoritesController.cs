using System.Security.Claims;
using System.Threading.Tasks;
using ExchangeHub.UserService.DTO;
using ExchangeHub.UserService.Queries.AddFavoriteCurrency;
using ExchangeHub.UserService.Queries.DeleteFavoriteCurrency;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeHub.UserService;

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