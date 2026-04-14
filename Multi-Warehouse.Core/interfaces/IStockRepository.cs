using Multi_Warehouse.Core.Models;

namespace Multi_Warehouse.Core.interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetAsync(int productId, int warehouseId);
        Task AddAsync(Stock stock);
    }
}
