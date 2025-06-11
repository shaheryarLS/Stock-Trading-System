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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockService.GetAllAsync();
            return Ok(stocks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _stockService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.StockId }, created);
        }
    }
}
