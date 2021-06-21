using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class PurchaseOrder
    {
        public int Id { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public string Potype { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
