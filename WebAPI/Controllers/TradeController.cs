using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;
using System.Security.Claims;

namespace Stock_Trading_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AccessPolicy")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll
        (
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = "TradeDate",
            [FromQuery] bool sortAsc = false
        )
        {
            var trades = await _tradeService.GetAllAsync();
            return Ok(trades);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTradeDto dto)
        {
            var trade = await _tradeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = trade.TradeId }, trade);
        }

        [HttpGet("secret")]
        public IActionResult Secret()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var claims = User.Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList();
            return Ok(new
            {
                Message = "This is a secret message from the TradeController!",
                UserId = userId,
                Claims = claims
            });
        }
    }

}
