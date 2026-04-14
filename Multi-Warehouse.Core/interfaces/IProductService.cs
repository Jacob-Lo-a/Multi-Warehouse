using Multi_Warehouse.Core.DTOs;

namespace Multi_Warehouse.Core.interfaces
{
    public interface IProductService
    {
        Task<List<ProductWithStockDto>> GetProductsWithStocksAsync();
        Task StockInAsync(StockInRequest request);
    }
}
