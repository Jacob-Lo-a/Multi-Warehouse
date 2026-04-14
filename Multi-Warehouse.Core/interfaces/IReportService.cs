namespace Multi_Warehouse.Core.interfaces
{
    public interface IReportService
    {
        Task<byte[]> ExportInventoryReportAsync();
        Task DailySalesReportAsync();
    }
}
