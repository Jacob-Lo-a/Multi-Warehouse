namespace Multi_Warehouse.API.Exceptions
{
    public class ProductOrWarehouseNotExist : Exception
    {
        public ProductOrWarehouseNotExist() : base ("產品或倉庫不存在") { }
    }
}
