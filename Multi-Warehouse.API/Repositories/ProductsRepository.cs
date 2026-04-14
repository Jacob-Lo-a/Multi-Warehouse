using Microsoft.EntityFrameworkCore;
using Multi_Warehouse.Core.DTOs;
using Multi_Warehouse.Core.interfaces;
using Multi_Warehouse.Core.Models;
namespace Multi_Warehouse.API.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AdvancedWmsDbContext _advancedWmsDbContext;

        public ProductsRepository(AdvancedWmsDbContext advancedWmsDbContext) 
        {
            _advancedWmsDbContext = advancedWmsDbContext;
        }

        public async Task<List<ProductWithStockDto>> GetProductsWithStocksAsync()
        {
            return await _advancedWmsDbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Include(p => p.Stocks)
            .ThenInclude(s => s.Warehouse)
            .Select(p => new ProductWithStockDto
            {
                SKU = p.Sku,
                ProductName = p.ProductName,
                CategoryName = p.Category.CategoryName,
                SupplierName = p.Supplier.SupplierName,
                Stocks = p.Stocks.Select(s => new WarehouseStockDto
                {
                    WarehouseName = s.Warehouse.WarehouseName,
                    Quantity = s.CurrentQuantity
                }).ToList()
            })
            .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int productId)
        {
            return await _advancedWmsDbContext.Products.AnyAsync(p => p.Id == productId);
        }
    }
}
