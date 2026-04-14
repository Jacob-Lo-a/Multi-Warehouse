using Multi_Warehouse.Core.DTOs;

namespace Multi_Warehouse.Core.interfaces
{
    public interface IStockService
    {
        Task StockInAsync(StockInRequest request);
    }
}
