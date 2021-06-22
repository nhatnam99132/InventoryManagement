using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class PurchaseOrder : DatetimeEntity
    {
        public int Id { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public string Potype { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
      

        public virtual Supplier Supplier { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
