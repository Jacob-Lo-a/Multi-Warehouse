using Multi_Warehouse.Core.DTOs;

namespace Multi_Warehouse.Core.interfaces
{
    public interface IReportRepository
    {
        Task<List<InventoryReportDto>> GetInventoryReportAsync();
        Task<List<InventoryReportDto>> GetLowInventoryReportAsync();
    }
}
