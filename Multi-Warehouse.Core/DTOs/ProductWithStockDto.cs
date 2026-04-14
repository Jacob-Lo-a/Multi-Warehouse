
namespace Multi_Warehouse.Core.DTOs
{
    public class ProductWithStockDto
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }

        public List<WarehouseStockDto> Stocks { get; set; }
    }
}
