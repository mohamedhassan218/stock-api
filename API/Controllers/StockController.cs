using API.Dtos.Stock;
using API.Helpers;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        // GET api/stock
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GelAllAsync(query);
            var stocksDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stocksDto);
        }

        // GET api/stock?id=x
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);
            return (stock == null) ? NotFound() : Ok(stock.ToStockDto());
        }

        // POST api/stock
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = createStockDto.ToStockDtoFromCreateDto();
            await _stockRepo.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        // PUT api/stock?id=x
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.UpdateAsync(id, updateStockDto);
            if (stock != null)
                return Ok(stock.ToStockDto());
            return NotFound();
        }

        // DELETE api/stock?id=x
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.DeleteAsync(id);
            if (stock != null)
                return NoContent();
            return NotFound();
        }
    }
}