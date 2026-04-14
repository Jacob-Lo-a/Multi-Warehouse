using Multi_Warehouse.Core.interfaces;
using Multi_Warehouse.Core.Models;

namespace Multi_Warehouse.API.Repositories
{
    public class StockLogRepository : IStockLogRepository
    {
        private readonly AdvancedWmsDbContext _advancedWmsDbContext;

        public StockLogRepository(AdvancedWmsDbContext advancedWmsDbContext)
        {
            _advancedWmsDbContext = advancedWmsDbContext;
        }

        public async Task AddAsync(StockLog log)
        {
            await _advancedWmsDbContext.StockLogs.AddAsync(log);
        }
    }
}
