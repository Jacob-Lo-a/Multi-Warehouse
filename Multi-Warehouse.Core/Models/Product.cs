using System;
using System.Collections.Generic;

namespace Multi_Warehouse.Core.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Sku { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public decimal BasePrice { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual Supplier Supplier { get; set; } = null!;
}
