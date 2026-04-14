
namespace Multi_Warehouse.Core.interfaces
{
    public interface IWarehouseRepository
    {
        Task<bool> ExistsAsync(int warehouseId);
    }
}
