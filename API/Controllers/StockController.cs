using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Stock;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var stocks = _context.Stock.ToList()
            .Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stock.Find(id);
            return (stock == null) ? NotFound() : Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockDto createStockDto)
        {
            var stock = createStockDto.ToStockDtoFromCreateDto();
            _context.Stock.Add(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var stock = _context.Stock.FirstOrDefault(s => s.Id == id);
            if (stock != null)
            {
                stock.Symbol = updateStockDto.Symbol;
                stock.CompanyName = updateStockDto.CompanyName;
                stock.Purchase = updateStockDto.Purchase;
                stock.LastDiv = updateStockDto.LastDiv;
                stock.Industry = updateStockDto.Industry;
                stock.MarketCap = updateStockDto.MarketCap;
                _context.SaveChanges();
                return Ok(stock.ToStockDto());
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stock = _context.Stock.FirstOrDefault(s => s.Id == id);

            if (stock != null)
            {
                _context.Stock.Remove(stock);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
    }
}