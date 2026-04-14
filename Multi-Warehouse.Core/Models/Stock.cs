using System;
using System.Collections.Generic;

namespace Multi_Warehouse.Core.Models;

public partial class Stock
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int WarehouseId { get; set; }

    public int CurrentQuantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
