using System.Security.Claims;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Application.Queries.GetAllCurrencies;
using ExchangeHub.FinanceService.Application.Queries.GetUserFavoriteCurrencies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeHub.FinanceService.Api.Controllers;

[ApiController]
[Route("api/finance")]
public class FinanceController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet("favorites")]
    public async Task<IActionResult> GetFavorites()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await mediator.Send(new GetUserFavoriteCurrenciesQuery(userId));
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllCurrenciesQuery());
        return Ok(result);
    }
}
