
namespace Multi_Warehouse.Core.DTOs
{
    public class StockInRequest
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
        public string Remark { get; set; }
    }
}
