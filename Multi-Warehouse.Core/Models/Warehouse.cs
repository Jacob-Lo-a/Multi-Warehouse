using System;
using System.Collections.Generic;

namespace Multi_Warehouse.Core.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string WarehouseName { get; set; } = null!;

    public string? Location { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
