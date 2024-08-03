using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Stock;
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

        public async Task<List<Stock>> GelAllAsync()
        {
            return await _context.Stock.ToListAsync();
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
            var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
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
    }
}