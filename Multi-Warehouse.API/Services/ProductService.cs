using Multi_Warehouse.API.Exceptions;
using Multi_Warehouse.Core.DTOs;
using Multi_Warehouse.Core.interfaces;
using Multi_Warehouse.Core.Models;
namespace Multi_Warehouse.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productRepo;
        private readonly IWarehouseRepository _warehouseRepo;
        private readonly IStockRepository _stockRepo;
        private readonly IStockLogRepository _logRepo;
        private readonly AdvancedWmsDbContext _context;
        private readonly ILogger<ProductService> _logger;
        public ProductService(
        IProductsRepository productRepo,
        IWarehouseRepository warehouseRepo,
        IStockRepository stockRepo,
        IStockLogRepository logRepo,
        AdvancedWmsDbContext context,
        ILogger<ProductService> logger)
        {
            _productRepo = productRepo;
            _warehouseRepo = warehouseRepo;
            _stockRepo = stockRepo;
            _logRepo = logRepo;
            _context = context;
            _logger = logger;
        }

        public async Task<List<ProductWithStockDto>> GetProductsWithStocksAsync()
        {

            return await _productRepo.GetProductsWithStocksAsync();
        }

        public async Task StockInAsync(StockInRequest request)
        {
            //  驗證
            if (!await _productRepo.ExistsAsync(request.ProductId) ||
                !await _warehouseRepo.ExistsAsync(request.WarehouseId))
            {
                throw new ProductOrWarehouseNotExist();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var stock = await _stockRepo.GetAsync(request.ProductId, request.WarehouseId);

                if (stock != null)
                {
                    stock.CurrentQuantity += request.Quantity;
                }
                else
                {
                    stock = new Stock
                    {
                        ProductId = request.ProductId,
                        WarehouseId = request.WarehouseId,
                        CurrentQuantity = request.Quantity
                    };

                    await _stockRepo.AddAsync(stock);
                }

                await _logRepo.AddAsync(new StockLog
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.WarehouseId,
                    ChangeQuantity = request.Quantity,
                    LogDate = DateTime.Now,
                    Remark = string.IsNullOrWhiteSpace(request.Remark)
                        ? "入庫"
                        : request.Remark
                });
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "交易失敗，資料已 Rollback");
                throw;
            }
        }
    }
}
