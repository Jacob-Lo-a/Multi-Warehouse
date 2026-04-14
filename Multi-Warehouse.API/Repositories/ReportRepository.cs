using Multi_Warehouse.Core.DTOs;
using Multi_Warehouse.Core.interfaces;
using Multi_Warehouse.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Multi_Warehouse.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AdvancedWmsDbContext _advancedWmsDbContext;

        public ReportRepository(AdvancedWmsDbContext advancedWmsDbContext)
        {
            _advancedWmsDbContext = advancedWmsDbContext;
        }
        // 
        public async Task<List<InventoryReportDto>> GetInventoryReportAsync()
        {
            return await _advancedWmsDbContext.Products
                .AsNoTracking()
                .Select(p => new InventoryReportDto
                {
                    ProductName = p.ProductName,
                    CategoryName = p.Category.CategoryName,
                    SupplierName = p.Supplier.SupplierName,

                    WarehouseStocks = p.Stocks.Select(s => new WarehouseStockDto
                    {
                        WarehouseName = s.Warehouse.WarehouseName,
                        Quantity = s.CurrentQuantity
                    }).ToList(),
                    
                    TotalQuantity = p.Stocks.Sum(s => s.CurrentQuantity)
                })
                .ToListAsync();
        }
       
        public async Task<List<InventoryReportDto>> GetLowInventoryReportAsync()
        {
            return await _advancedWmsDbContext.Products
                .AsNoTracking()
                .Where(p => p.Stocks.Sum(s => s.CurrentQuantity) < 10)
                .Select(p => new InventoryReportDto
                {
                    ProductName = p.ProductName,
                    CategoryName = p.Category.CategoryName,
                    SupplierName = p.Supplier.SupplierName,

                    WarehouseStocks = p.Stocks.Select(s => new WarehouseStockDto
                    {
                        WarehouseName = s.Warehouse.WarehouseName,
                        Quantity = s.CurrentQuantity
                    }).ToList(),

                    TotalQuantity = p.Stocks.Sum(s => s.CurrentQuantity)
                })
                .ToListAsync();
        }
        
    }
}
