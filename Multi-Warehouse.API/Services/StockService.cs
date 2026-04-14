using Multi_Warehouse.Core.DTOs;
using Multi_Warehouse.Core.interfaces;

namespace Multi_Warehouse.API.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        public StockService(IStockRepository stockRepository) 
        { 
            _stockRepository = stockRepository;
        }

        public async Task StockInAsync(StockInRequest request)
        {
            
        }
    }
}
