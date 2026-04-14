using Multi_Warehouse.Core.interfaces;
using Multi_Warehouse.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Multi_Warehouse.API.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AdvancedWmsDbContext _advancedWmsDbContext;

        public WarehouseRepository(AdvancedWmsDbContext advancedWmsDbContext)
        {
            _advancedWmsDbContext = advancedWmsDbContext;
        }

        public async Task<bool> ExistsAsync(int warehouseId)
        {
            return await _advancedWmsDbContext.Warehouses.AnyAsync(w => w.Id == warehouseId);
        }
    }
}
