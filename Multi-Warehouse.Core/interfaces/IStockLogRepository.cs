using Multi_Warehouse.Core.Models;

namespace Multi_Warehouse.Core.interfaces
{
    public interface IStockLogRepository
    {
        Task AddAsync(StockLog log);
    }
}
