namespace Multi_Warehouse.Core.DTOs
{
    public class InventoryReportDto
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }

        public List<WarehouseStockDto> WarehouseStocks { get; set; } // 各倉庫
        public int TotalQuantity { get; set; }
    }
}
