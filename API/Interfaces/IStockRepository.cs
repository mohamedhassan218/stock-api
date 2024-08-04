using API.Dtos.Stock;
using API.Models;

namespace API.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GelAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int id);
    }
}