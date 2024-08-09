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

    }
}