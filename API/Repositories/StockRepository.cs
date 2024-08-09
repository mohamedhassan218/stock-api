using API.Data;
using API.Dtos.Stock;
using API.Helpers;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GelAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stock.Include(c => c.Comments).ThenInclude(a => a.UserNavigation).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
                stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
            if (stock != null)
            {
                _context.Stock.Remove(stock);
                await _context.SaveChangesAsync();
                return stock;
            }
            return null;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if (stock != null)
                return stock;
            return null;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto)
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
                return stock;
            }
            return null;
        }

        public async Task<bool> StockExist(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
            return (stock == null) ? false : true;
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Symbol == symbol);
            return (stock != null) ? stock : null;
        }
    }
}