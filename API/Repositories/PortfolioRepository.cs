using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<List<Stock>> GetUserPortfolio(User user)
        {
            return await _context.Portfolios.Where(p => p.UserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeleteAsync(User user, string symbol)
        {
            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(p => p.UserId == user.Id && p.Stock.Symbol.ToLower() == symbol.ToLower());
            if (portfolio != null)
            {
                _context.Portfolios.Remove(portfolio);
                await _context.SaveChangesAsync();
            }
            return portfolio;
        }
    }
}