using API.Extensions;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<User> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<User> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Portfolio>>> GetUserPortfolio()
        {
            var userName = User.GetUsername();

            var user = await _userManager.FindByNameAsync(userName);

            var userPortoflio = await _portfolioRepository.GetUserPortfolio(user);

            return Ok(userPortoflio);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Portfolio>> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByNameAsync(username);

            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)
                return BadRequest("Stock not found!!");

            var userPortoflio = await _portfolioRepository.GetUserPortfolio(user);

            if (userPortoflio.Any(p => p.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Stock already added!!!");

            var portfolio = new Portfolio
            {
                UserId = user.Id,
                StockId = stock.Id
            };

            await _portfolioRepository.CreateAsync(portfolio);

            return (portfolio != null) ? Created() : StatusCode(500, "Something went wrong!!");
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByNameAsync(username);

            var userPortoflio = await _portfolioRepository.GetUserPortfolio(user);

            var filteredStock = userPortoflio.Where(p => p.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count == 0)
                return BadRequest("Stock is not in your portfolio!!!");

            await _portfolioRepository.DeleteAsync(user, symbol);
            return Ok();
        }
    }
}