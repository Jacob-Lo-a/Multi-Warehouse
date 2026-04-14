using System;
using System.Collections.Generic;

namespace Multi_Warehouse.Core.Models;

public partial class StockLog
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int WarehouseId { get; set; }

    public int ChangeQuantity { get; set; }

    public DateTime LogDate { get; set; }

    public string? Remark { get; set; }
}
