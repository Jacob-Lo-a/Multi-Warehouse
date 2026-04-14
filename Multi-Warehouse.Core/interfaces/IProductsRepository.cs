using Multi_Warehouse.Core.DTOs;

namespace Multi_Warehouse.Core.interfaces
{
    public interface IProductsRepository
    {
        Task<List<ProductWithStockDto>> GetProductsWithStocksAsync();
        Task<bool> ExistsAsync(int productId);
    }
}
