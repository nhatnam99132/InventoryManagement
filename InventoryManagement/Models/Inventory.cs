using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Inventory
    {
        public int? WarehouseId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
