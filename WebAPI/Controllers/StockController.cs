using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;

namespace Stock_Trading_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll
        (
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = "Symbol",
            [FromQuery] bool sortAsc = false
        )
        {
            var stocks = await _stockService.GetAllAsync(page, pageSize, sortBy, sortAsc);
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetById(int id)
        {
            var stock = await _stockService.GetByIdAsync(id);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpGet("symbol/{symbol}")]
        public async Task<IActionResult> GetBySymbol(string symbol)
        {
            var stock = await _stockService.GetBySymbolAsync(symbol);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _stockService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.StockId }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _stockService.DeleteStockAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto stockDto)
        {
            var result = await _stockService.UpdateStockAsync(id, stockDto);
            if (!result)
                return NotFound();

            return NoContent();
        }


    }
}
