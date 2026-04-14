using System;
using System.Collections.Generic;

namespace Multi_Warehouse.Core.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactPhone { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
