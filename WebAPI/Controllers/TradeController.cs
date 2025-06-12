using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;

namespace Stock_Trading_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }

}
