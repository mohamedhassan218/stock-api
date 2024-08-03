using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Stock;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tmpStocks = await _context.Stock.ToListAsync();
            var stocks = _context.Stock.Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        // GET api/stock?id=
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            return (stock == null) ? NotFound() : Ok(stock.ToStockDto());
        }

        // POST api/stock
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto)
        {
            var stock = createStockDto.ToStockDtoFromCreateDto();
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        // PUT api/stock?id=
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
            if (stock != null)
            {
                stock.Symbol = updateStockDto.Symbol;
                stock.CompanyName = updateStockDto.CompanyName;
                stock.Purchase = updateStockDto.Purchase;
                stock.LastDiv = updateStockDto.LastDiv;
                stock.Industry = updateStockDto.Industry;
                stock.MarketCap = updateStockDto.MarketCap;
                await _context.SaveChangesAsync();
                return Ok(stock.ToStockDto());
            }
            return NotFound();
        }

        // DELETE api/stock?id=
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);

            if (stock != null)
            {
                _context.Stock.Remove(stock);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
    }
}