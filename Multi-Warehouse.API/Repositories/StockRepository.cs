using Microsoft.EntityFrameworkCore;
using Multi_Warehouse.Core.interfaces;
using Multi_Warehouse.Core.Models;
namespace Multi_Warehouse.API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AdvancedWmsDbContext _advancedWmsDbContext;
        public StockRepository(AdvancedWmsDbContext advancedWmsDbContext) 
        {
            _advancedWmsDbContext = advancedWmsDbContext;
        }
        public async Task<Stock?> GetAsync(int productId, int warehouseId)
        {
            return await _advancedWmsDbContext.Stocks
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId);
        }

        public async Task AddAsync(Stock stock)
        {
            await _advancedWmsDbContext.Stocks.AddAsync(stock);
        }
    }
}
