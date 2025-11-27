using Microsoft.AspNetCore.Mvc;
using Suno.MercadoBitcoin.Application.Interfaces;
using Suno.MercadoBitcoin.Application.Queries;

namespace Suno.MercadoBitcoin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MercadoBitcoinController : ControllerBase
{
    private readonly IAccountPositionService _service;

    public MercadoBitcoinController(IAccountPositionService service)
    {
        _service = service;
    }

    [HttpGet("positions")]
    public async Task<IActionResult> GetPositions(
        [FromQuery] string accountId,
        [FromHeader(Name = "Authorization")] string bearerToken)
    {
        if (string.IsNullOrWhiteSpace(bearerToken))
            return Unauthorized("Authorization: Bearer <token> header is required");

        var token = bearerToken.Replace("Bearer ", "");

        var result = await _service.GetPositionsAsync(accountId, token);

        return Ok(result);
    }
}
