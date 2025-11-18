using System.Security.Claims;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Queries.GetUserFavoriteCurrencies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeHub.FinanceService;

[ApiController]
[Route("api/finance")]
public class FinanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public FinanceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet("favorites")]
    public async Task<IActionResult> GetFavorites()
    {
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);


        var result = await _mediator.Send(new GetUserFavoriteCurrenciesQuery(userId));
        return Ok(result);
    }
}
